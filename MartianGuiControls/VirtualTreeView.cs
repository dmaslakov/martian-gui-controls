// Published under http://www.opensource.org/licenses/BSD-3-Clause license, see license.txt file for details.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MartianGuiControls.Native;
using System.Diagnostics;

namespace MartianGuiControls
{
	// About Tree-View Controls: http://msdn.microsoft.com/en-us/library/bb760017.aspx
	[ToolboxItem(true)]
	[DesignTimeVisible]
	public partial class VirtualTreeView: TreeView
	{
		private const string _LOADING = "Loading...";

		private bool _needClearAccess = false;
		private bool _virtualByDefault = false;
		private bool _makeRealAutomatically = false;
		private bool _delayedTimerEnabled = false;
		private string _delayedLoadingText = _LOADING;

		private readonly Set<IntPtr> _virtualNodes = new Set<IntPtr>();
		private readonly List<IntPtr> _delayedLoadNodes = new List<IntPtr>(); // parent nodes for loading children
		private readonly List<IntPtr> _delayedLoadingNodes = new List<IntPtr>(); // temporary nodes "Loading...", will be removed after children loaded

		public VirtualTreeView()
		{
			InitializeComponent();
		}

		[Browsable(true)]
		[Category("Behavior")]
		[Description("Indicates whether new added nodes are marked as virtual.")]
		[DefaultValue(false)]
		public bool NewNodesVirtual
		{
			get { return _virtualByDefault; }
			set { _virtualByDefault = value; }
		}

		[Browsable(true)]
		[Category("Behavior")]
		[Description("Indicates whether nodes will be converted into real nodes after any child node added.")]
		[DefaultValue(false)]
		public bool PromoteToRealNode
		{
			get { return _makeRealAutomatically; }
			set { _makeRealAutomatically = value; }
		}

		[Browsable(true)]
		[Category("Behavior")]
		[Description("Number of milliseconds to delay before starting loading of real nodes. Zero delay means immediate children loading.")]
		[DefaultValue(0)]
		public int ChildrenLoadDelay
		{
			get
			{
				return _delayedTimerEnabled ? delayedLoadingTimer.Interval : 0;
			}
			set
			{
				if (value <= 0)
				{
					_delayedTimerEnabled = false;
				}
				else
				{
					_delayedTimerEnabled = true;
					delayedLoadingTimer.Interval = value;
				}
			}
		}

		[Browsable(true)]
		[Category("Appearance")]
		[Description("Indicates a delay before starting loading of real nodes. Zero delay means immediate children loading.")]
		[Localizable(true)]
		[DefaultValue(_LOADING)]
		public string DelayedLoadingText
		{
			get { return _delayedLoadingText; }
			set { _delayedLoadingText = value; }
		}

		[Browsable(true)]
		[Category("Behavior")]
		[Description("Occurs when a node children nodes is about to be populated.")]
		public event VirtualTreeViewCreateChildrenHandler CreateRealChildren;

		protected override void WndProc(ref Message m)
		{
			if (_needClearAccess)
			{
				base.WndProc(ref m);
				return;
			}

			switch (m.Msg)
			{
			case (int)TVM.TVM_INSERTITEM:
				unsafe
				{
					IntPtr hItem = IntPtr.Zero;

					TVINSERTSTRUCT *ins = (TVINSERTSTRUCT*)m.LParam;
					if ((ins->item.mask & (int)TVIF.TVIF_CHILDREN) == (int)TVIF.TVIF_CHILDREN && ins->item.cChildren == CallbackTypes.I_CHILDRENCALLBACK)
					{
						// already virtual
						base.WndProc(ref m);
						hItem = m.Result;
					}
					else if (_virtualByDefault)
					{
						// trigger a "virtual" marker
						TVINSERTSTRUCT ins2 = *ins;
						ins2.item.mask |= (int)TVIF.TVIF_CHILDREN;
						ins2.item.cChildren = CallbackTypes.I_CHILDRENCALLBACK;

						Message m2 = m;
						m2.LParam = new IntPtr(&ins2);
						base.WndProc(ref m2);
						hItem = m.Result = m2.Result;
					}
					else
					{
						// not virtual and have not make it virtual
						base.WndProc(ref m);
					}

					PromoteToReal(ins->hParent);

					if (hItem != IntPtr.Zero)
						_virtualNodes.Add(m.Result);
				}
				return;

			case (int)TVM.TVM_DELETEITEM:
				NMTREEVIEW tv = (NMTREEVIEW)m.GetLParam(typeof(NMTREEVIEW));
				_virtualNodes.Remove(tv.itemOld.hItem);
				break;

			case (int)WM.WM_NOTIFY + (int)WM.WM_REFLECTED:
				unsafe
				{
					NMTVDISPINFO* di = (NMTVDISPINFO*)m.LParam;
					if (di->hdr.code == (int)TVN.TVN_GETDISPINFO)
					{
						if ((di->item.mask & (int)TVIF.TVIF_CHILDREN) == (int)TVIF.TVIF_CHILDREN && _virtualNodes.Contains(di->item.hItem))
						{
							di->item.cChildren = 1; // virtual node is always expandable
						}
					}
				}
				break;
			} // switch

			base.WndProc(ref m);
		}

		protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
		{
			base.OnBeforeExpand(e);
			if (!e.Cancel && IsVirtualNode(e.Node) && e.Node.Nodes.Count == 0)
				StartLoading(e.Node);
		}

		private void StartLoading(TreeNode n)
		{
			if (_delayedLoadNodes.Contains(n.Handle))
				return; // already scheduled for loading

			if (!_delayedTimerEnabled)
			{
				// immediate load
				OnCreateRealChildren(new VirtualTreeViewCreateChildrenEventArgs(n));
			}
			else
			{
				// delayed load
				IntPtr loadingItem = AddLoadingNode(n);
				_delayedLoadNodes.Add(n.Handle);
				_delayedLoadingNodes.Add(loadingItem);
				delayedLoadingTimer.Start();
			}
		}

		private IntPtr AddLoadingNode(TreeNode parent)
		{
			// Temporary nodes "Loading..." must not go through the logic in overloaded WndProc.
			try
			{
				_needClearAccess = true;
				return parent.Nodes.Add(_delayedLoadingText).Handle;
			}
			finally
			{
				_needClearAccess = false;
			}
		}

		protected virtual void OnCreateRealChildren(VirtualTreeViewCreateChildrenEventArgs e)
		{
			VirtualTreeViewCreateChildrenHandler h = CreateRealChildren;
			if (h != null)
			{
				h(this, e);
				PromoteToReal(e.Node.Handle);
			}
		}

		private void delayedLoadingTimer_Tick(object sender, EventArgs e)
		{
			// remove temporary nodes "Loading..." (these nodes may affect handlers for CreateRealChildren event, so delete them first)
			foreach (IntPtr tmpItem in _delayedLoadingNodes)
			{
				TreeNode n = TreeNode.FromHandle(this, tmpItem);
				if (n != null)
					n.Remove();
			}
			_delayedLoadingNodes.Clear();

			// fill children nodes
			foreach (IntPtr hItem in _delayedLoadNodes)
			{
				TreeNode n = TreeNode.FromHandle(this, hItem);
				if (n != null)
					OnCreateRealChildren(new VirtualTreeViewCreateChildrenEventArgs(n));
				// TODO make something if loading takes a lot of time when there are many nodes
			}
			_delayedLoadNodes.Clear();

			// the job is done, stop timer
			delayedLoadingTimer.Stop();
		}

		public void SetVirtualNode(TreeNode n, bool isVirtual)
		{
			Trace.Assert(n.TreeView == this, "Can't manage nodes from other tree.");
			SetVirtualNode(n.Handle, isVirtual);
		}

		private void SetVirtualNode(IntPtr hItem, bool isVirtual)
		{
			bool exists = _virtualNodes.Contains(hItem);
			if (isVirtual && exists)
				return; // already virtual

			if (!isVirtual && !exists)
				return; // already not virtual

			// TODO how make it correct?
			int children = isVirtual ? CallbackTypes.I_CHILDRENCALLBACK : (TreeNode.FromHandle(this, hItem).Nodes.Count > 0 ? 1 : 0);
			//int children = isVirtual ? (int)CallbackTypes.I_CHILDRENCALLBACK : -2 /*(n.Nodes.Count > 0 ? 1 : 0)*/;
			if (!NativeFunc.SetTVItemChildren(Handle, hItem, children))
				throw new InvalidOperationException("Can't set TreeView node properties.");

			if (isVirtual)
				_virtualNodes.Add(hItem);
			else
				_virtualNodes.Remove(hItem);
		}

		public bool IsVirtualNode(TreeNode n)
		{
			Trace.Assert(n.TreeView == this, "Can't manage nodes from other tree.");
			return _virtualNodes.Contains(n.Handle);
		}

		private void PromoteToReal(IntPtr hItem)
		{
			if (_makeRealAutomatically && _virtualNodes.Contains(hItem))
			{
				SetVirtualNode(hItem, false);
			}
		}
	} // class VirtualTreeView

	public class VirtualTreeViewCreateChildrenEventArgs: EventArgs
	{
		private readonly TreeNode _node;

		public VirtualTreeViewCreateChildrenEventArgs(TreeNode node)
		{
			_node = node;
		}

		public TreeNode Node
		{
			get { return _node; }
		}
	}

	public delegate void VirtualTreeViewCreateChildrenHandler(object sender, VirtualTreeViewCreateChildrenEventArgs e);
}
