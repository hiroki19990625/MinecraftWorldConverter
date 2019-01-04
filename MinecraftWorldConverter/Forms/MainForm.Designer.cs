namespace MinecraftWorldConverter.Forms
{
    partial class MainForm
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.button = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.state = new System.Windows.Forms.Label();
            this.oldList = new System.Windows.Forms.ComboBox();
            this.newList = new System.Windows.Forms.ComboBox();
            this.arrow = new System.Windows.Forms.Label();
            this.refarence = new System.Windows.Forms.Button();
            this.labelOld = new System.Windows.Forms.Label();
            this.labelNew = new System.Windows.Forms.Label();
            this.finishCheckWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ツールTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nBTViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.valueSpliterSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggerLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(12, 52);
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(360, 19);
            this.textBox.TabIndex = 0;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(10, 37);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(47, 12);
            this.label.TabIndex = 1;
            this.label.Text = "level.dat";
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(297, 226);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(75, 23);
            this.button.TabIndex = 2;
            this.button.Text = "変換";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 193);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(360, 10);
            this.progressBar.TabIndex = 3;
            // 
            // state
            // 
            this.state.AutoSize = true;
            this.state.Location = new System.Drawing.Point(10, 178);
            this.state.Name = "state";
            this.state.Size = new System.Drawing.Size(29, 12);
            this.state.TabIndex = 4;
            this.state.Text = "状態";
            // 
            // oldList
            // 
            this.oldList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.oldList.FormattingEnabled = true;
            this.oldList.Location = new System.Drawing.Point(12, 128);
            this.oldList.Name = "oldList";
            this.oldList.Size = new System.Drawing.Size(121, 20);
            this.oldList.TabIndex = 5;
            // 
            // newList
            // 
            this.newList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.newList.FormattingEnabled = true;
            this.newList.Location = new System.Drawing.Point(251, 128);
            this.newList.Name = "newList";
            this.newList.Size = new System.Drawing.Size(121, 20);
            this.newList.TabIndex = 6;
            // 
            // arrow
            // 
            this.arrow.AutoSize = true;
            this.arrow.Font = new System.Drawing.Font("MS UI Gothic", 20F);
            this.arrow.Location = new System.Drawing.Point(169, 128);
            this.arrow.Name = "arrow";
            this.arrow.Size = new System.Drawing.Size(39, 27);
            this.arrow.TabIndex = 7;
            this.arrow.Text = "➡";
            // 
            // refarence
            // 
            this.refarence.Location = new System.Drawing.Point(12, 77);
            this.refarence.Name = "refarence";
            this.refarence.Size = new System.Drawing.Size(75, 23);
            this.refarence.TabIndex = 8;
            this.refarence.Text = "参照";
            this.refarence.UseVisualStyleBackColor = true;
            this.refarence.Click += new System.EventHandler(this.refarence_Click);
            // 
            // labelOld
            // 
            this.labelOld.AutoSize = true;
            this.labelOld.Location = new System.Drawing.Point(12, 113);
            this.labelOld.Name = "labelOld";
            this.labelOld.Size = new System.Drawing.Size(101, 12);
            this.labelOld.TabIndex = 9;
            this.labelOld.Text = "変換前のフォーマット";
            // 
            // labelNew
            // 
            this.labelNew.AutoSize = true;
            this.labelNew.Location = new System.Drawing.Point(249, 113);
            this.labelNew.Name = "labelNew";
            this.labelNew.Size = new System.Drawing.Size(101, 12);
            this.labelNew.TabIndex = 10;
            this.labelNew.Text = "変換後のフォーマット";
            // 
            // finishCheckWorker
            // 
            this.finishCheckWorker.WorkerSupportsCancellation = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ツールTToolStripMenuItem,
            this.helpHToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(384, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ツールTToolStripMenuItem
            // 
            this.ツールTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nBTViewerToolStripMenuItem,
            this.valueSpliterSToolStripMenuItem,
            this.loggerLToolStripMenuItem});
            this.ツールTToolStripMenuItem.Name = "ツールTToolStripMenuItem";
            this.ツールTToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.ツールTToolStripMenuItem.Text = "ツール(&T)";
            // 
            // nBTViewerToolStripMenuItem
            // 
            this.nBTViewerToolStripMenuItem.Name = "nBTViewerToolStripMenuItem";
            this.nBTViewerToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.nBTViewerToolStripMenuItem.Text = "NBT Viewer(&N)";
            this.nBTViewerToolStripMenuItem.Click += new System.EventHandler(this.nBTViewerToolStripMenuItem_Click);
            // 
            // valueSpliterSToolStripMenuItem
            // 
            this.valueSpliterSToolStripMenuItem.Name = "valueSpliterSToolStripMenuItem";
            this.valueSpliterSToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.valueSpliterSToolStripMenuItem.Text = "Bit Spliter(&S)";
            this.valueSpliterSToolStripMenuItem.Click += new System.EventHandler(this.valueSpliterSToolStripMenuItem_Click);
            // 
            // loggerLToolStripMenuItem
            // 
            this.loggerLToolStripMenuItem.Name = "loggerLToolStripMenuItem";
            this.loggerLToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.loggerLToolStripMenuItem.Text = "Logger(&L)";
            this.loggerLToolStripMenuItem.Click += new System.EventHandler(this.loggerLToolStripMenuItem_Click);
            // 
            // helpHToolStripMenuItem
            // 
            this.helpHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.licenseLToolStripMenuItem});
            this.helpHToolStripMenuItem.Name = "helpHToolStripMenuItem";
            this.helpHToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.helpHToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // licenseLToolStripMenuItem
            // 
            this.licenseLToolStripMenuItem.Name = "licenseLToolStripMenuItem";
            this.licenseLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.licenseLToolStripMenuItem.Text = "ライセンス(&L)";
            this.licenseLToolStripMenuItem.Click += new System.EventHandler(this.licenseLToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.labelNew);
            this.Controls.Add(this.labelOld);
            this.Controls.Add(this.refarence);
            this.Controls.Add(this.arrow);
            this.Controls.Add(this.newList);
            this.Controls.Add(this.oldList);
            this.Controls.Add(this.state);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.button);
            this.Controls.Add(this.label);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MinecraftWorldConverter";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label state;
        private System.Windows.Forms.ComboBox oldList;
        private System.Windows.Forms.ComboBox newList;
        private System.Windows.Forms.Label arrow;
        private System.Windows.Forms.Button refarence;
        private System.Windows.Forms.Label labelOld;
        private System.Windows.Forms.Label labelNew;
        private System.ComponentModel.BackgroundWorker finishCheckWorker;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ツールTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nBTViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem valueSpliterSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggerLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licenseLToolStripMenuItem;
    }
}