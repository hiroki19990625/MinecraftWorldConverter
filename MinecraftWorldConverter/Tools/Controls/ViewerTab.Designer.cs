namespace MinecraftWorldConverter.Tools.Controls
{
    partial class ViewerTab
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.value = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Value";
            // 
            // value
            // 
            this.value.Location = new System.Drawing.Point(262, 17);
            this.value.MaxLength = 2100000000;
            this.value.Multiline = true;
            this.value.Name = "value";
            this.value.ReadOnly = true;
            this.value.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.value.Size = new System.Drawing.Size(503, 425);
            this.value.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tree";
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(5, 17);
            this.treeView.Name = "treeView";
            this.treeView.ShowNodeToolTips = true;
            this.treeView.Size = new System.Drawing.Size(226, 425);
            this.treeView.TabIndex = 4;
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            // 
            // ViewerTab
            // 
            this.Controls.Add(this.label2);
            this.Controls.Add(this.value);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView);
            this.Name = "ViewerTab";
            this.Size = new System.Drawing.Size(770, 450);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox value;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView;
    }
}
