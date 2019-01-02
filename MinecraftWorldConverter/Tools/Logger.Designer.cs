namespace MinecraftWorldConverter.Tools
{
    partial class Logger
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
            this.loggerData = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.displayDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allClearCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loggerData
            // 
            this.loggerData.Location = new System.Drawing.Point(12, 27);
            this.loggerData.Multiline = true;
            this.loggerData.Name = "loggerData";
            this.loggerData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.loggerData.Size = new System.Drawing.Size(660, 522);
            this.loggerData.TabIndex = 0;
            this.loggerData.WordWrap = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayDToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // displayDToolStripMenuItem
            // 
            this.displayDToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allClearCToolStripMenuItem});
            this.displayDToolStripMenuItem.Name = "displayDToolStripMenuItem";
            this.displayDToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.displayDToolStripMenuItem.Text = "Display(&D)";
            // 
            // allClearCToolStripMenuItem
            // 
            this.allClearCToolStripMenuItem.Name = "allClearCToolStripMenuItem";
            this.allClearCToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.allClearCToolStripMenuItem.Text = "All Clear(&C)";
            this.allClearCToolStripMenuItem.Click += new System.EventHandler(this.allClearCToolStripMenuItem_Click);
            // 
            // Logger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 561);
            this.Controls.Add(this.loggerData);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Logger";
            this.Text = "Logger";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox loggerData;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem displayDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allClearCToolStripMenuItem;
    }
}