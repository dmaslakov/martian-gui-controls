// Published under http://www.opensource.org/licenses/BSD-3-Clause license, see license.txt file for details.

using System;
using System.Windows.Forms;
using MartianGuiControls;

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

	}
}
