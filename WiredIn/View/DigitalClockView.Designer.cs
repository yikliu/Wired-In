namespace Wired_In.View
{
    partial class DigitalClockView
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
            this.digClock = new Owf.Controls.DigitalDisplayControl();
            this.SuspendLayout();
            // 
            // digClock
            // 
            this.digClock.BackColor = System.Drawing.Color.Transparent;
            this.digClock.DigitColor = System.Drawing.Color.LimeGreen;
            this.digClock.Location = new System.Drawing.Point(39, 63);
            this.digClock.Name = "digClock";
            this.digClock.DigitText = "00:00";
            this.digClock.Size = new System.Drawing.Size(695, 383);
            this.digClock.TabIndex = 0;
            // 
            // DigitalClockView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.digClock);
            this.Name = "DigitalClockView";
            this.Size = new System.Drawing.Size(780, 510);
            this.ResumeLayout(false);

        }

        #endregion

        private Owf.Controls.DigitalDisplayControl digClock;
    }
}
