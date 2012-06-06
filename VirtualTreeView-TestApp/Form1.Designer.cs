namespace VirtualTreeViewTestApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btReproduceIssue = new System.Windows.Forms.Button();
			this.btClearTree = new System.Windows.Forms.Button();
			this.cbReproduceIssue = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.numLoadDelay = new System.Windows.Forms.NumericUpDown();
			this.tbNodeStatus = new System.Windows.Forms.TextBox();
			this.btRemoveSelectedNode = new System.Windows.Forms.Button();
			this.btAddChildNode = new System.Windows.Forms.Button();
			this.btAddRootNode = new System.Windows.Forms.Button();
			this.xbAutoPromote = new System.Windows.Forms.CheckBox();
			this.xbAutoVirtual = new System.Windows.Forms.CheckBox();
			this.vtv = new MartianGuiControls.VirtualTreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numLoadDelay)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.btReproduceIssue);
			this.splitContainer1.Panel1.Controls.Add(this.btClearTree);
			this.splitContainer1.Panel1.Controls.Add(this.cbReproduceIssue);
			this.splitContainer1.Panel1.Controls.Add(this.label2);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1.Controls.Add(this.numLoadDelay);
			this.splitContainer1.Panel1.Controls.Add(this.tbNodeStatus);
			this.splitContainer1.Panel1.Controls.Add(this.btRemoveSelectedNode);
			this.splitContainer1.Panel1.Controls.Add(this.btAddChildNode);
			this.splitContainer1.Panel1.Controls.Add(this.btAddRootNode);
			this.splitContainer1.Panel1.Controls.Add(this.xbAutoPromote);
			this.splitContainer1.Panel1.Controls.Add(this.xbAutoVirtual);
			this.splitContainer1.Panel1MinSize = 150;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.vtv);
			this.splitContainer1.Panel2MinSize = 50;
			this.splitContainer1.Size = new System.Drawing.Size(550, 400);
			this.splitContainer1.SplitterDistance = 162;
			this.splitContainer1.TabIndex = 1;
			// 
			// btReproduceIssue
			// 
			this.btReproduceIssue.Location = new System.Drawing.Point(12, 307);
			this.btReproduceIssue.Name = "btReproduceIssue";
			this.btReproduceIssue.Size = new System.Drawing.Size(129, 23);
			this.btReproduceIssue.TabIndex = 12;
			this.btReproduceIssue.Text = "Reproduce issue:";
			this.btReproduceIssue.UseVisualStyleBackColor = true;
			this.btReproduceIssue.Click += new System.EventHandler(this.btReproduceIssue_Click);
			// 
			// btClearTree
			// 
			this.btClearTree.Location = new System.Drawing.Point(12, 195);
			this.btClearTree.Name = "btClearTree";
			this.btClearTree.Size = new System.Drawing.Size(129, 23);
			this.btClearTree.TabIndex = 11;
			this.btClearTree.Text = "Clear tree";
			this.btClearTree.UseVisualStyleBackColor = true;
			this.btClearTree.Click += new System.EventHandler(this.btClearTree_Click);
			// 
			// cbReproduceIssue
			// 
			this.cbReproduceIssue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbReproduceIssue.FormattingEnabled = true;
			this.cbReproduceIssue.Location = new System.Drawing.Point(12, 336);
			this.cbReproduceIssue.Name = "cbReproduceIssue";
			this.cbReproduceIssue.Size = new System.Drawing.Size(130, 21);
			this.cbReproduceIssue.TabIndex = 9;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 245);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Node type:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 55);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(102, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Delay (milliseconds):";
			// 
			// numLoadDelay
			// 
			this.numLoadDelay.Location = new System.Drawing.Point(49, 71);
			this.numLoadDelay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numLoadDelay.Name = "numLoadDelay";
			this.numLoadDelay.Size = new System.Drawing.Size(92, 20);
			this.numLoadDelay.TabIndex = 6;
			this.numLoadDelay.ValueChanged += new System.EventHandler(this.numLoadDelay_ValueChanged);
			// 
			// tbNodeStatus
			// 
			this.tbNodeStatus.Location = new System.Drawing.Point(12, 261);
			this.tbNodeStatus.Name = "tbNodeStatus";
			this.tbNodeStatus.ReadOnly = true;
			this.tbNodeStatus.Size = new System.Drawing.Size(129, 20);
			this.tbNodeStatus.TabIndex = 5;
			// 
			// btRemoveSelectedNode
			// 
			this.btRemoveSelectedNode.Location = new System.Drawing.Point(12, 165);
			this.btRemoveSelectedNode.Name = "btRemoveSelectedNode";
			this.btRemoveSelectedNode.Size = new System.Drawing.Size(130, 23);
			this.btRemoveSelectedNode.TabIndex = 4;
			this.btRemoveSelectedNode.Text = "Remove selected node";
			this.btRemoveSelectedNode.UseVisualStyleBackColor = true;
			this.btRemoveSelectedNode.Click += new System.EventHandler(this.btRemoveSelectedNode_Click);
			// 
			// btAddChildNode
			// 
			this.btAddChildNode.Location = new System.Drawing.Point(12, 136);
			this.btAddChildNode.Name = "btAddChildNode";
			this.btAddChildNode.Size = new System.Drawing.Size(130, 23);
			this.btAddChildNode.TabIndex = 3;
			this.btAddChildNode.Text = "Add child node";
			this.btAddChildNode.UseVisualStyleBackColor = true;
			this.btAddChildNode.Click += new System.EventHandler(this.btAddChildNode_Click);
			// 
			// btAddRootNode
			// 
			this.btAddRootNode.Location = new System.Drawing.Point(12, 107);
			this.btAddRootNode.Name = "btAddRootNode";
			this.btAddRootNode.Size = new System.Drawing.Size(129, 23);
			this.btAddRootNode.TabIndex = 2;
			this.btAddRootNode.Text = "Add root node";
			this.btAddRootNode.UseVisualStyleBackColor = true;
			this.btAddRootNode.Click += new System.EventHandler(this.btAddRootNode_Click);
			// 
			// xbAutoPromote
			// 
			this.xbAutoPromote.AutoSize = true;
			this.xbAutoPromote.Location = new System.Drawing.Point(12, 35);
			this.xbAutoPromote.Name = "xbAutoPromote";
			this.xbAutoPromote.Size = new System.Drawing.Size(129, 17);
			this.xbAutoPromote.TabIndex = 1;
			this.xbAutoPromote.Text = "Promote to real nodes";
			this.xbAutoPromote.UseVisualStyleBackColor = true;
			this.xbAutoPromote.CheckedChanged += new System.EventHandler(this.xbAutoPromote_CheckedChanged);
			// 
			// xbAutoVirtual
			// 
			this.xbAutoVirtual.AutoSize = true;
			this.xbAutoVirtual.Location = new System.Drawing.Point(12, 12);
			this.xbAutoVirtual.Name = "xbAutoVirtual";
			this.xbAutoVirtual.Size = new System.Drawing.Size(129, 17);
			this.xbAutoVirtual.TabIndex = 0;
			this.xbAutoVirtual.Text = "New nodes are virtual";
			this.xbAutoVirtual.UseVisualStyleBackColor = true;
			this.xbAutoVirtual.CheckedChanged += new System.EventHandler(this.xbAutoVirtual_CheckedChanged);
			// 
			// vtv
			// 
			this.vtv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.vtv.ImageIndex = 0;
			this.vtv.ImageList = this.imageList1;
			this.vtv.Location = new System.Drawing.Point(0, 0);
			this.vtv.Name = "vtv";
			this.vtv.SelectedImageIndex = 0;
			this.vtv.Size = new System.Drawing.Size(384, 400);
			this.vtv.TabIndex = 0;
			this.vtv.CreateRealChildren += new MartianGuiControls.VirtualTreeViewCreateChildrenHandler(this.virtualTreeView1_CreateRealChildren);
			this.vtv.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.vtv_AfterExpand);
			this.vtv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.virtualTreeView1_AfterSelect);
			this.vtv.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.virtualTreeView1_NodeMouseClick);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(550, 400);
			this.Controls.Add(this.splitContainer1);
			this.MinimumSize = new System.Drawing.Size(300, 350);
			this.Name = "Form1";
			this.Text = "VirtualTreeView Test Application";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numLoadDelay)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private MartianGuiControls.VirtualTreeView vtv;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.CheckBox xbAutoPromote;
		private System.Windows.Forms.CheckBox xbAutoVirtual;
		private System.Windows.Forms.Button btRemoveSelectedNode;
		private System.Windows.Forms.Button btAddChildNode;
		private System.Windows.Forms.Button btAddRootNode;
		private System.Windows.Forms.TextBox tbNodeStatus;
		private System.Windows.Forms.NumericUpDown numLoadDelay;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbReproduceIssue;
		private System.Windows.Forms.Button btClearTree;
		private System.Windows.Forms.Button btReproduceIssue;
		private System.Windows.Forms.ImageList imageList1;
    }
}

