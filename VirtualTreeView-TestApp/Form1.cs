// Published under http://www.opensource.org/licenses/BSD-3-Clause license, see license.txt file for details.

using System;
using System.Windows.Forms;
using MartianGuiControls;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic;

namespace VirtualTreeViewTestApp
{
	public partial class Form1: Form
	{
		public Form1()
		{
			InitializeComponent();

			// set initial values from form designer
			vtv.NewNodesVirtual = xbAutoVirtual.Checked;
			vtv.PromoteToRealNode = xbAutoPromote.Checked;
			vtv.ChildrenLoadDelay = Convert.ToInt32(numLoadDelay.Value);

			// fill helpers for how-reproduce-issue
			LoadIssueItems();
		}

		private Dictionary<int, Delegate> _issueHandlers = new Dictionary<int, Delegate>();

		private delegate void ReproduceIssueHandler();

		private void LoadIssueItems()
		{
			MethodInfo[] miList = this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			foreach (MethodInfo mi in miList)
			{
				if (mi.Name.StartsWith("Issue_"))
				{
					object[] attrList = mi.GetCustomAttributes(typeof(DescriptionAttribute), true);
					if (attrList != null && attrList.Length > 0)
					{
						foreach (DescriptionAttribute attr in attrList)
						{
							int idx = cbReproduceIssue.Items.Add(attr.Description);
							_issueHandlers.Add(idx, Delegate.CreateDelegate(typeof(ReproduceIssueHandler), this, mi));
						}
					}
				}
			}
		}

		void virtualTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
				e.Node.Remove();
		}

		void virtualTreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			//e.Node.Nodes.Add("child");
		}

		private void virtualTreeView1_CreateRealChildren(object sender, VirtualTreeViewCreateChildrenEventArgs e)
		{
			e.Node.Nodes.Add("child");
		}

		private void xbAutoPromote_CheckedChanged(object sender, EventArgs e)
		{
			vtv.PromoteToRealNode = ((CheckBox)sender).Checked;
		}

		private void xbAutoVirtual_CheckedChanged(object sender, EventArgs e)
		{
			vtv.NewNodesVirtual = ((CheckBox)sender).Checked;
		}

		private int _nodesCnt = 0;

		private void btAddRootNode_Click(object sender, EventArgs e)
		{
			vtv.Nodes.Add("root #" + _nodesCnt.ToString());
			++_nodesCnt;
		}

		private void btAddChildNode_Click(object sender, EventArgs e)
		{
			if (vtv.SelectedNode != null)
			{
				vtv.SelectedNode.Nodes.Add("child #" + _nodesCnt.ToString());
				++_nodesCnt;
			}
		}

		private void btRemoveSelectedNode_Click(object sender, EventArgs e)
		{
			if (vtv.SelectedNode != null)
			{
				vtv.SelectedNode.Remove();
			}
		}

		private void virtualTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			tbNodeStatus.Text = vtv.IsVirtualNode(e.Node) ? "virtual" : "real";
		}

		private void numLoadDelay_ValueChanged(object sender, EventArgs e)
		{
			vtv.ChildrenLoadDelay = Convert.ToInt32(((NumericUpDown)sender).Value);
		}

		private void btClearTree_Click(object sender, EventArgs e)
		{
			vtv.Nodes.Clear();
		}

		private void btReproduceIssue_Click(object sender, EventArgs e)
		{
			int idx = cbReproduceIssue.SelectedIndex;
			if (idx >= 0 && _issueHandlers.ContainsKey(idx))
			{
				_issueHandlers[idx].DynamicInvoke();
			}
		}

		#region Issues reproduction
		// All methods must be:
		// - static
		// - name starts with Issue_
		// - take single argument of type VirtualTreeView
		// - marked with Description attribute

		[Description("flicker: mass load")]
		private void Issue_MassLoad_Flicker()
		{
			if (_flickerTimer != null)
			{
				MessageBox.Show("Flicker test is already running.");
				return;
			}
			_flickerTimer = new Timer();
			_flickerTimer.Tick += new EventHandler(_flickerTimer_Tick);
			_flickerTimer.Interval = 100; // 10 times per second
			_flickerTimer.Tag = 0;
			_flickerTimer.Start();
		}

		private const int _flickerCount = 10000;
		private Timer _flickerTimer;

		void _flickerTimer_Tick(object sender, EventArgs e)
		{
			int cnt = (int)_flickerTimer.Tag;
			int max = Math.Min(cnt + 100, _flickerCount);

			for (; cnt < max; ++cnt)
			{
				vtv.Nodes.Add("mass load #" + cnt.ToString());
			}

			if (cnt < _flickerCount)
			{
				_flickerTimer.Tag = cnt;
			}
			else
			{
				// destroy timer
				_flickerTimer.Dispose();
				_flickerTimer = null;
			}
		}


		#endregion
	}
}
