namespace PercentilePlugin
{
    partial class PercentileUi
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.logView = new System.Windows.Forms.ListView();
            this.timeCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.levelCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.messageCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastCheckLbl = new System.Windows.Forms.Label();
            this.updateBtn = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.autoUpdateChbx = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // logView
            // 
            this.logView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.timeCol,
            this.levelCol,
            this.messageCol});
            this.logView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logView.Location = new System.Drawing.Point(0, 93);
            this.logView.Name = "logView";
            this.logView.Size = new System.Drawing.Size(906, 432);
            this.logView.TabIndex = 0;
            this.logView.UseCompatibleStateImageBehavior = false;
            this.logView.View = System.Windows.Forms.View.Details;
            this.logView.VirtualMode = true;
            this.logView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.logView_RetrieveVirtualItem);
            // 
            // timeCol
            // 
            this.timeCol.Text = "Time";
            this.timeCol.Width = 106;
            // 
            // levelCol
            // 
            this.levelCol.Text = "Level";
            // 
            // messageCol
            // 
            this.messageCol.Text = "Message";
            this.messageCol.Width = 736;
            // 
            // lastCheckLbl
            // 
            this.lastCheckLbl.AutoSize = true;
            this.lastCheckLbl.Location = new System.Drawing.Point(3, 5);
            this.lastCheckLbl.Name = "lastCheckLbl";
            this.lastCheckLbl.Size = new System.Drawing.Size(160, 13);
            this.lastCheckLbl.TabIndex = 1;
            this.lastCheckLbl.Text = "Last Updated: NOT-AVAILABLE";
            // 
            // updateBtn
            // 
            this.updateBtn.Location = new System.Drawing.Point(3, 21);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(75, 23);
            this.updateBtn.TabIndex = 3;
            this.updateBtn.Text = "Update";
            this.updateBtn.UseVisualStyleBackColor = true;
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // autoUpdateChbx
            // 
            this.autoUpdateChbx.AutoSize = true;
            this.autoUpdateChbx.Location = new System.Drawing.Point(85, 26);
            this.autoUpdateChbx.Name = "autoUpdateChbx";
            this.autoUpdateChbx.Size = new System.Drawing.Size(134, 17);
            this.autoUpdateChbx.TabIndex = 6;
            this.autoUpdateChbx.Text = "Auto Update On Start?";
            this.autoUpdateChbx.UseVisualStyleBackColor = true;
            this.autoUpdateChbx.CheckedChanged += new System.EventHandler(this.autoUpdateChbx_CheckedChanged);
            // 
            // PercentileUi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.autoUpdateChbx);
            this.Controls.Add(this.updateBtn);
            this.Controls.Add(this.lastCheckLbl);
            this.Controls.Add(this.logView);
            this.Name = "PercentileUi";
            this.Size = new System.Drawing.Size(906, 525);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView logView;
        private System.Windows.Forms.ColumnHeader timeCol;
        private System.Windows.Forms.ColumnHeader levelCol;
        private System.Windows.Forms.ColumnHeader messageCol;
        private System.Windows.Forms.Button updateBtn;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        public System.Windows.Forms.Label lastCheckLbl;
        private System.Windows.Forms.CheckBox autoUpdateChbx;
    }
}
