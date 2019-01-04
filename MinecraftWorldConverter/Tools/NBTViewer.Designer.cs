namespace MinecraftWorldConverter.Tools
{
    partial class NBTViewer
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabControlMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formZLibZToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromGZipGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endianEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bigEndianBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.littleEndianLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formRawBinaryRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.tabControlMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.displayDToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(884, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileOToolStripMenuItem,
            this.endianEToolStripMenuItem});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            this.fileFToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.fileFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // openFileOToolStripMenuItem
            // 
            this.openFileOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formZLibZToolStripMenuItem,
            this.fromGZipGToolStripMenuItem,
            this.formRawBinaryRToolStripMenuItem});
            this.openFileOToolStripMenuItem.Name = "openFileOToolStripMenuItem";
            this.openFileOToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openFileOToolStripMenuItem.Text = "開く(&O)";
            // 
            // displayDToolStripMenuItem
            // 
            this.displayDToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeAllAToolStripMenuItem});
            this.displayDToolStripMenuItem.Name = "displayDToolStripMenuItem";
            this.displayDToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.displayDToolStripMenuItem.Text = "表示(&D)";
            // 
            // closeAllAToolStripMenuItem
            // 
            this.closeAllAToolStripMenuItem.Name = "closeAllAToolStripMenuItem";
            this.closeAllAToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeAllAToolStripMenuItem.Text = "全て閉じる(&A)";
            this.closeAllAToolStripMenuItem.Click += new System.EventHandler(this.closeAllAToolStripMenuItem_Click);
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 24);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(884, 537);
            this.tabControl.TabIndex = 0;
            // 
            // tabControlMenu
            // 
            this.tabControlMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeCToolStripMenuItem});
            this.tabControlMenu.Name = "tabControlMenu";
            this.tabControlMenu.Size = new System.Drawing.Size(120, 26);
            // 
            // closeCToolStripMenuItem
            // 
            this.closeCToolStripMenuItem.Name = "closeCToolStripMenuItem";
            this.closeCToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.closeCToolStripMenuItem.Text = "閉じる(&C)";
            this.closeCToolStripMenuItem.Click += new System.EventHandler(this.閉じるCToolStripMenuItem_Click);
            // 
            // formZLibZToolStripMenuItem
            // 
            this.formZLibZToolStripMenuItem.Name = "formZLibZToolStripMenuItem";
            this.formZLibZToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.formZLibZToolStripMenuItem.Text = "form ZLib(&Z)";
            // 
            // fromGZipGToolStripMenuItem
            // 
            this.fromGZipGToolStripMenuItem.Name = "fromGZipGToolStripMenuItem";
            this.fromGZipGToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.fromGZipGToolStripMenuItem.Text = "from GZip(&G)";
            // 
            // endianEToolStripMenuItem
            // 
            this.endianEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bigEndianBToolStripMenuItem,
            this.littleEndianLToolStripMenuItem});
            this.endianEToolStripMenuItem.Name = "endianEToolStripMenuItem";
            this.endianEToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.endianEToolStripMenuItem.Text = "エンディアン(&E)";
            // 
            // bigEndianBToolStripMenuItem
            // 
            this.bigEndianBToolStripMenuItem.Checked = true;
            this.bigEndianBToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bigEndianBToolStripMenuItem.Name = "bigEndianBToolStripMenuItem";
            this.bigEndianBToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.bigEndianBToolStripMenuItem.Text = "ビッグエンディアン(&B)";
            // 
            // littleEndianLToolStripMenuItem
            // 
            this.littleEndianLToolStripMenuItem.Name = "littleEndianLToolStripMenuItem";
            this.littleEndianLToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.littleEndianLToolStripMenuItem.Text = "リトルエンディアン(&L)";
            // 
            // formRawBinaryRToolStripMenuItem
            // 
            this.formRawBinaryRToolStripMenuItem.Name = "formRawBinaryRToolStripMenuItem";
            this.formRawBinaryRToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.formRawBinaryRToolStripMenuItem.Text = "form Raw Binary(&R)";
            // 
            // NBTViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip);
            this.Name = "NBTViewer";
            this.Text = "NBTViewer";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabControlMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileOToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ContextMenuStrip tabControlMenu;
        private System.Windows.Forms.ToolStripMenuItem closeCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formZLibZToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromGZipGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formRawBinaryRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem endianEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bigEndianBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem littleEndianLToolStripMenuItem;
    }
}