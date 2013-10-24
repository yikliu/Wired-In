namespace WiredIn.Visualization.View
{
    partial class ProgressBarView
    {        
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param viewName="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.bar = new WiredIn.Visualization.View.MyProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bar
            // 
            this.bar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bar.Location = new System.Drawing.Point(0, 0);
            this.bar.Maximum = 1000;
            this.bar.Minimum = 0;
            this.bar.Name = "bar";
            this.bar.ProgressBarColor = System.Drawing.Color.Green;
            this.bar.Size = new System.Drawing.Size(950, 105);
            this.bar.TabIndex = 0;
            this.bar.Value = 1000;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 1;
            // 
            // ProgressBarView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bar);
            this.Name = "ProgressBarView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyProgressBar bar;
        private System.Windows.Forms.Label label1;
    }
}
