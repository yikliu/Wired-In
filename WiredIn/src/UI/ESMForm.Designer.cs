using MetroFramework.Forms;
namespace WiredIn.UI
{
    partial class ESMForm :  MetroForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param viewName="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                j_logger.Dispose();
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
            this.tbxReasonForDelay = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.lblTimeSpan = new MetroFramework.Controls.MetroLabel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.cbReason = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.rbReluctant = new MetroFramework.Controls.MetroRadioButton();
            this.rbForgot = new MetroFramework.Controls.MetroRadioButton();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.tbInfluence = new MetroFramework.Controls.MetroTrackBar();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // tbxReasonForDelay
            // 
            this.tbxReasonForDelay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbxReasonForDelay.BackColor = System.Drawing.Color.AliceBlue;
            this.tbxReasonForDelay.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbxReasonForDelay.Location = new System.Drawing.Point(162, 170);
            this.tbxReasonForDelay.MaxLength = 32767;
            this.tbxReasonForDelay.Multiline = true;
            this.tbxReasonForDelay.Name = "tbxReasonForDelay";
            this.tbxReasonForDelay.PasswordChar = '\0';
            this.tbxReasonForDelay.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxReasonForDelay.SelectedText = "";
            this.tbxReasonForDelay.Size = new System.Drawing.Size(679, 52);
            this.tbxReasonForDelay.TabIndex = 0;
            this.tbxReasonForDelay.UseCustomBackColor = true;
            this.tbxReasonForDelay.UseSelectable = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(162, 134);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(542, 25);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "What were you doing during that time? (Or select any from below)";
            // 
            // metroLabel2
            // 
            this.metroLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.Location = new System.Drawing.Point(162, 98);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(283, 25);
            this.metroLabel2.TabIndex = 2;
            this.metroLabel2.Text = "You were away from your task for ";
            // 
            // lblTimeSpan
            // 
            this.lblTimeSpan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTimeSpan.AutoSize = true;
            this.lblTimeSpan.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblTimeSpan.Location = new System.Drawing.Point(436, 98);
            this.lblTimeSpan.Name = "lblTimeSpan";
            this.lblTimeSpan.Size = new System.Drawing.Size(76, 25);
            this.lblTimeSpan.TabIndex = 3;
            this.lblTimeSpan.Text = "<TIME>";
            // 
            // metroButton1
            // 
            this.metroButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroButton1.BackColor = System.Drawing.Color.Transparent;
            this.metroButton1.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.metroButton1.Location = new System.Drawing.Point(388, 423);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(228, 54);
            this.metroButton1.TabIndex = 4;
            this.metroButton1.Text = "Continue";
            this.metroButton1.UseCustomBackColor = true;
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // cbReason
            // 
            this.cbReason.FormattingEnabled = true;
            this.cbReason.ItemHeight = 23;
            this.cbReason.Items.AddRange(new object[] {
            "Facebook/Twitter/..",
            "Watching an Youtube/online video",
            "Texting (IM or Mobile)",
            "Having a conversation with another person",
            "Reading news stories online",
            "Looking for information",
            "Having a snack"});
            this.cbReason.Location = new System.Drawing.Point(162, 226);
            this.cbReason.Name = "cbReason";
            this.cbReason.PromptText = "Select one";
            this.cbReason.Size = new System.Drawing.Size(679, 29);
            this.cbReason.TabIndex = 2;
            this.cbReason.UseSelectable = true;
            this.cbReason.SelectedIndexChanged += new System.EventHandler(this.cbReason_SelectedIndexChanged);
            // 
            // metroLabel3
            // 
            this.metroLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel3.Location = new System.Drawing.Point(162, 284);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(513, 25);
            this.metroLabel3.TabIndex = 1;
            this.metroLabel3.Text = "Were you reluctant to come back to tasks or did you forget to?";
            // 
            // rbReluctant
            // 
            this.rbReluctant.AutoSize = true;
            this.rbReluctant.Location = new System.Drawing.Point(162, 310);
            this.rbReluctant.Name = "rbReluctant";
            this.rbReluctant.Size = new System.Drawing.Size(107, 17);
            this.rbReluctant.TabIndex = 5;
            this.rbReluctant.Text = "I was reluctant";
            this.rbReluctant.UseSelectable = true;
            this.rbReluctant.CheckedChanged += new System.EventHandler(this.rbReluctant_CheckedChanged);
            // 
            // rbForgot
            // 
            this.rbForgot.AutoSize = true;
            this.rbForgot.Location = new System.Drawing.Point(298, 310);
            this.rbForgot.Name = "rbForgot";
            this.rbForgot.Size = new System.Drawing.Size(68, 17);
            this.rbForgot.TabIndex = 5;
            this.rbForgot.Text = "I forgot";
            this.rbForgot.UseSelectable = true;
            this.rbForgot.CheckedChanged += new System.EventHandler(this.rbForgot_CheckedChanged);
            // 
            // metroLabel4
            // 
            this.metroLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel4.Location = new System.Drawing.Point(162, 349);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(642, 25);
            this.metroLabel4.TabIndex = 1;
            this.metroLabel4.Text = "How much does the visualization influence your decison to come back to tasks?";
            // 
            // tbInfluence
            // 
            this.tbInfluence.BackColor = System.Drawing.Color.Transparent;
            this.tbInfluence.Location = new System.Drawing.Point(162, 378);
            this.tbInfluence.Name = "tbInfluence";
            this.tbInfluence.Size = new System.Drawing.Size(679, 23);
            this.tbInfluence.TabIndex = 6;
            this.tbInfluence.Text = "metroTrackBar1";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(162, 404);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(63, 19);
            this.metroLabel5.TabIndex = 7;
            this.metroLabel5.Text = "Not at all";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(787, 404);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(71, 19);
            this.metroLabel6.TabIndex = 7;
            this.metroLabel6.Text = "Very much";
            // 
            // ESMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 500);
            this.ControlBox = false;
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.tbInfluence);
            this.Controls.Add(this.rbForgot);
            this.Controls.Add(this.rbReluctant);
            this.Controls.Add(this.cbReason);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.lblTimeSpan);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.tbxReasonForDelay);
            this.Name = "ESMForm";
            this.Padding = new System.Windows.Forms.Padding(23, 60, 23, 20);
            this.Resizable = false;
            this.Text = "Quick Question";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ESMForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox tbxReasonForDelay;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel lblTimeSpan;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroComboBox cbReason;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroRadioButton rbReluctant;
        private MetroFramework.Controls.MetroRadioButton rbForgot;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroTrackBar tbInfluence;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel6;

    }
}