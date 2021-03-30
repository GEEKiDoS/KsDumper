namespace KsDumperClient
{
    partial class Dumper
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.refreshMenuBtn = new System.Windows.Forms.ToolStripButton();
            this.hideSystemProcessMenuBtn = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.logsTextBox = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dumpMainModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processList = new KsDumperClient.Utility.ProcessListView();
            this.PIDHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PathHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BaseAddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EntryPointHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImageSizeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImageTypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.moduleList = new KsDumperClient.Utility.ModuleListView();
            this.ModuleAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ModulePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ModuleEntry = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ModuleSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dumpThisModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModuleType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshMenuBtn,
            this.hideSystemProcessMenuBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(2);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 2);
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(1004, 29);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // refreshMenuBtn
            // 
            this.refreshMenuBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.refreshMenuBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshMenuBtn.Name = "refreshMenuBtn";
            this.refreshMenuBtn.Size = new System.Drawing.Size(56, 21);
            this.refreshMenuBtn.Text = "Refresh";
            this.refreshMenuBtn.Click += new System.EventHandler(this.refreshMenuBtn_Click);
            // 
            // hideSystemProcessMenuBtn
            // 
            this.hideSystemProcessMenuBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.hideSystemProcessMenuBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.hideSystemProcessMenuBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hideSystemProcessMenuBtn.Name = "hideSystemProcessMenuBtn";
            this.hideSystemProcessMenuBtn.Size = new System.Drawing.Size(150, 21);
            this.hideSystemProcessMenuBtn.Text = "Show System Processes";
            this.hideSystemProcessMenuBtn.Click += new System.EventHandler(this.hideSystemProcessMenuBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logsTextBox);
            this.groupBox1.Location = new System.Drawing.Point(5, 541);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(992, 149);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logs";
            // 
            // logsTextBox
            // 
            this.logsTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.logsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logsTextBox.Location = new System.Drawing.Point(12, 18);
            this.logsTextBox.Name = "logsTextBox";
            this.logsTextBox.ReadOnly = true;
            this.logsTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.logsTextBox.Size = new System.Drawing.Size(968, 182);
            this.logsTextBox.TabIndex = 0;
            this.logsTextBox.Text = "";
            this.logsTextBox.TextChanged += new System.EventHandler(this.logsTextBox_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dumpMainModuleToolStripMenuItem,
            this.toolStripSeparator1,
            this.openInExplorerToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(194, 54);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // dumpMainModuleToolStripMenuItem
            // 
            this.dumpMainModuleToolStripMenuItem.Name = "dumpMainModuleToolStripMenuItem";
            this.dumpMainModuleToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.dumpMainModuleToolStripMenuItem.Text = "Dump Main Module";
            this.dumpMainModuleToolStripMenuItem.Click += new System.EventHandler(this.dumpMainModuleToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
            // 
            // openInExplorerToolStripMenuItem
            // 
            this.openInExplorerToolStripMenuItem.Name = "openInExplorerToolStripMenuItem";
            this.openInExplorerToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.openInExplorerToolStripMenuItem.Text = "Open In Explorer";
            this.openInExplorerToolStripMenuItem.Click += new System.EventHandler(this.openInExplorerToolStripMenuItem_Click);
            // 
            // processList
            // 
            this.processList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PIDHeader,
            this.NameHeader,
            this.PathHeader,
            this.BaseAddressHeader,
            this.EntryPointHeader,
            this.ImageSizeHeader,
            this.ImageTypeHeader});
            this.processList.ContextMenuStrip = this.contextMenuStrip1;
            this.processList.FullRowSelect = true;
            this.processList.HideSelection = false;
            this.processList.Location = new System.Drawing.Point(5, 26);
            this.processList.MultiSelect = false;
            this.processList.Name = "processList";
            this.processList.Size = new System.Drawing.Size(992, 335);
            this.processList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.processList.TabIndex = 2;
            this.processList.UseCompatibleStateImageBehavior = false;
            this.processList.View = System.Windows.Forms.View.Details;
            this.processList.SelectedIndexChanged += new System.EventHandler(this.processList_SelectedIndexChanged);
            // 
            // PIDHeader
            // 
            this.PIDHeader.Text = "PID";
            this.PIDHeader.Width = 76;
            // 
            // NameHeader
            // 
            this.NameHeader.Text = "Name";
            this.NameHeader.Width = 143;
            // 
            // PathHeader
            // 
            this.PathHeader.Text = "Path";
            this.PathHeader.Width = 375;
            // 
            // BaseAddressHeader
            // 
            this.BaseAddressHeader.Text = "Base Address";
            this.BaseAddressHeader.Width = 106;
            // 
            // EntryPointHeader
            // 
            this.EntryPointHeader.Text = "Entry Point";
            this.EntryPointHeader.Width = 106;
            // 
            // ImageSizeHeader
            // 
            this.ImageSizeHeader.Text = "Image Size";
            this.ImageSizeHeader.Width = 88;
            // 
            // ImageTypeHeader
            // 
            this.ImageTypeHeader.Text = "Image Type";
            this.ImageTypeHeader.Width = 72;
            // 
            // moduleList
            // 
            this.moduleList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ModuleAddress,
            this.ModulePath,
            this.ModuleEntry,
            this.ModuleSize,
            this.ModuleType});
            this.moduleList.ContextMenuStrip = this.contextMenuStrip2;
            this.moduleList.Location = new System.Drawing.Point(5, 367);
            this.moduleList.Name = "moduleList";
            this.moduleList.Size = new System.Drawing.Size(987, 168);
            this.moduleList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.moduleList.TabIndex = 6;
            this.moduleList.UseCompatibleStateImageBehavior = false;
            this.moduleList.View = System.Windows.Forms.View.Details;
            this.moduleList.FullRowSelect = true;
            this.moduleList.HideSelection = false;
            this.moduleList.MultiSelect = false;
            // 
            // ModuleAddress
            // 
            this.ModuleAddress.Text = "Base Address";
            this.ModuleAddress.Width = 221;
            // 
            // ModulePath
            // 
            this.ModulePath.Text = "Path";
            this.ModulePath.Width = 372;
            // 
            // ModuleEntry
            // 
            this.ModuleEntry.DisplayIndex = 3;
            this.ModuleEntry.Text = "Entry Point";
            this.ModuleEntry.Width = 150;
            // 
            // ModuleSize
            // 
            this.ModuleSize.DisplayIndex = 2;
            this.ModuleSize.Text = "Image Size";
            this.ModuleSize.Width = 150;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dumpThisModuleToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(184, 26);
            // 
            // dumpThisModuleToolStripMenuItem
            // 
            this.dumpThisModuleToolStripMenuItem.Name = "dumpThisModuleToolStripMenuItem";
            this.dumpThisModuleToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.dumpThisModuleToolStripMenuItem.Text = "Dump this module";
            this.dumpThisModuleToolStripMenuItem.Click += new System.EventHandler(this.dumpThisModuleToolStripMenuItem_Click);
            // 
            // ModuleType
            // 
            this.ModuleType.Text = "Image Type";
            this.ModuleType.Width = 72;
            // 
            // Dumper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 698);
            this.Controls.Add(this.moduleList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.processList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Dumper";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KsDumper";
            this.Load += new System.EventHandler(this.Dumper_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private KsDumperClient.Utility.ProcessListView processList;
        private KsDumperClient.Utility.ModuleListView moduleList;
        private System.Windows.Forms.ColumnHeader PIDHeader;
        private System.Windows.Forms.ColumnHeader NameHeader;
        private System.Windows.Forms.ColumnHeader PathHeader;
        private System.Windows.Forms.ColumnHeader BaseAddressHeader;
        private System.Windows.Forms.ColumnHeader EntryPointHeader;
        private System.Windows.Forms.ColumnHeader ImageSizeHeader;
        private System.Windows.Forms.ColumnHeader ImageTypeHeader;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton refreshMenuBtn;
        private System.Windows.Forms.ToolStripButton hideSystemProcessMenuBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox logsTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dumpMainModuleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem openInExplorerToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader ModuleAddress;
        private System.Windows.Forms.ColumnHeader ModulePath;
        private System.Windows.Forms.ColumnHeader ModuleEntry;
        private System.Windows.Forms.ColumnHeader ModuleSize;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem dumpThisModuleToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader ModuleType;
    }
}

