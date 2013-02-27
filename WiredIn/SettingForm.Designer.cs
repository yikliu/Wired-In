namespace WiredIn
{
    partial class SettingForm
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
            this.gbSize = new System.Windows.Forms.GroupBox();
            this.rbFullScreen = new System.Windows.Forms.RadioButton();
            this.rbSizeSmall = new System.Windows.Forms.RadioButton();
            this.gbImagery = new System.Windows.Forms.GroupBox();
            this.rdbProgbar = new System.Windows.Forms.RadioButton();
            this.rdbFlower = new System.Windows.Forms.RadioButton();
            this.cbTopMost = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbWhiteList = new System.Windows.Forms.GroupBox();
            this.lbxWhiteList = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lbl_title = new System.Windows.Forms.Label();
            this.crosshair = new ManagedWinapi.Crosshair();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbCondition = new System.Windows.Forms.GroupBox();
            this.rbPunishment = new System.Windows.Forms.RadioButton();
            this.rbReward = new System.Windows.Forms.RadioButton();
            this.gbSize.SuspendLayout();
            this.gbImagery.SuspendLayout();
            this.gbWhiteList.SuspendLayout();
            this.gbCondition.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSize
            // 
            this.gbSize.Controls.Add(this.rbFullScreen);
            this.gbSize.Controls.Add(this.rbSizeSmall);
            this.gbSize.Location = new System.Drawing.Point(9, 10);
            this.gbSize.Margin = new System.Windows.Forms.Padding(2);
            this.gbSize.Name = "gbSize";
            this.gbSize.Padding = new System.Windows.Forms.Padding(2);
            this.gbSize.Size = new System.Drawing.Size(370, 63);
            this.gbSize.TabIndex = 1;
            this.gbSize.TabStop = false;
            this.gbSize.Text = "Size";
            // 
            // rbFullScreen
            // 
            this.rbFullScreen.AutoSize = true;
            this.rbFullScreen.Location = new System.Drawing.Point(192, 27);
            this.rbFullScreen.Margin = new System.Windows.Forms.Padding(2);
            this.rbFullScreen.Name = "rbFullScreen";
            this.rbFullScreen.Size = new System.Drawing.Size(87, 19);
            this.rbFullScreen.TabIndex = 2;
            this.rbFullScreen.Text = "Full Screen";
            this.rbFullScreen.UseVisualStyleBackColor = true;
            this.rbFullScreen.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rbSizeSmall
            // 
            this.rbSizeSmall.AutoSize = true;
            this.rbSizeSmall.Location = new System.Drawing.Point(80, 27);
            this.rbSizeSmall.Margin = new System.Windows.Forms.Padding(2);
            this.rbSizeSmall.Name = "rbSizeSmall";
            this.rbSizeSmall.Size = new System.Drawing.Size(57, 19);
            this.rbSizeSmall.TabIndex = 0;
            this.rbSizeSmall.Text = "Small";
            this.rbSizeSmall.UseVisualStyleBackColor = true;
            this.rbSizeSmall.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // gbImagery
            // 
            this.gbImagery.Controls.Add(this.rdbProgbar);
            this.gbImagery.Controls.Add(this.rdbFlower);
            this.gbImagery.Location = new System.Drawing.Point(9, 77);
            this.gbImagery.Margin = new System.Windows.Forms.Padding(2);
            this.gbImagery.Name = "gbImagery";
            this.gbImagery.Padding = new System.Windows.Forms.Padding(2);
            this.gbImagery.Size = new System.Drawing.Size(370, 63);
            this.gbImagery.TabIndex = 2;
            this.gbImagery.TabStop = false;
            this.gbImagery.Text = "Imagery";
            // 
            // rdbProgbar
            // 
            this.rdbProgbar.AutoSize = true;
            this.rdbProgbar.Location = new System.Drawing.Point(192, 27);
            this.rdbProgbar.Margin = new System.Windows.Forms.Padding(2);
            this.rdbProgbar.Name = "rdbProgbar";
            this.rdbProgbar.Size = new System.Drawing.Size(96, 19);
            this.rdbProgbar.TabIndex = 1;
            this.rdbProgbar.TabStop = true;
            this.rdbProgbar.Text = "Progress Bar";
            this.rdbProgbar.UseVisualStyleBackColor = true;
            this.rdbProgbar.CheckedChanged += new System.EventHandler(this.rdbProgbar_CheckedChanged);
            // 
            // rdbFlower
            // 
            this.rdbFlower.AutoSize = true;
            this.rdbFlower.Location = new System.Drawing.Point(80, 27);
            this.rdbFlower.Margin = new System.Windows.Forms.Padding(2);
            this.rdbFlower.Name = "rdbFlower";
            this.rdbFlower.Size = new System.Drawing.Size(62, 19);
            this.rdbFlower.TabIndex = 0;
            this.rdbFlower.Text = "Flower";
            this.rdbFlower.UseVisualStyleBackColor = true;
            this.rdbFlower.CheckedChanged += new System.EventHandler(this.rdbFlower_CheckedChanged);
            // 
            // cbTopMost
            // 
            this.cbTopMost.AutoSize = true;
            this.cbTopMost.Location = new System.Drawing.Point(10, 145);
            this.cbTopMost.Margin = new System.Windows.Forms.Padding(2);
            this.cbTopMost.Name = "cbTopMost";
            this.cbTopMost.Size = new System.Drawing.Size(77, 19);
            this.cbTopMost.TabIndex = 3;
            this.cbTopMost.Text = "Top Most";
            this.cbTopMost.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(101, 502);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(196, 40);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbWhiteList
            // 
            this.gbWhiteList.Controls.Add(this.lbxWhiteList);
            this.gbWhiteList.Controls.Add(this.btnDelete);
            this.gbWhiteList.Controls.Add(this.btnAdd);
            this.gbWhiteList.Controls.Add(this.lbl_title);
            this.gbWhiteList.Controls.Add(this.crosshair);
            this.gbWhiteList.Location = new System.Drawing.Point(11, 233);
            this.gbWhiteList.Margin = new System.Windows.Forms.Padding(2);
            this.gbWhiteList.Name = "gbWhiteList";
            this.gbWhiteList.Padding = new System.Windows.Forms.Padding(2);
            this.gbWhiteList.Size = new System.Drawing.Size(369, 268);
            this.gbWhiteList.TabIndex = 5;
            this.gbWhiteList.TabStop = false;
            this.gbWhiteList.Text = "White List";
            // 
            // lbxWhiteList
            // 
            this.lbxWhiteList.FormattingEnabled = true;
            this.lbxWhiteList.Location = new System.Drawing.Point(1, 66);
            this.lbxWhiteList.Margin = new System.Windows.Forms.Padding(2);
            this.lbxWhiteList.Name = "lbxWhiteList";
            this.lbxWhiteList.Size = new System.Drawing.Size(364, 199);
            this.lbxWhiteList.TabIndex = 5;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.113208F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(80, 42);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(56, 19);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.113208F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(9, 42);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 19);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Location = new System.Drawing.Point(6, 15);
            this.lbl_title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(90, 15);
            this.lbl_title.TabIndex = 2;
            this.lbl_title.Text = "<Un-specified>";
            // 
            // crosshair
            // 
            this.crosshair.Location = new System.Drawing.Point(313, 30);
            this.crosshair.Name = "crosshair";
            this.crosshair.Size = new System.Drawing.Size(39, 31);
            this.crosshair.TabIndex = 1;
            this.toolTip1.SetToolTip(this.crosshair, "Drag crosshair onto the window to add ");
            this.crosshair.CrosshairDragged += new System.EventHandler(this.crosshair1_CrosshairDragged);
            this.crosshair.CrosshairDragging += new System.EventHandler(this.crosshair1_CrosshairDragging);
            // 
            // gbCondition
            // 
            this.gbCondition.Controls.Add(this.rbPunishment);
            this.gbCondition.Controls.Add(this.rbReward);
            this.gbCondition.Location = new System.Drawing.Point(11, 170);
            this.gbCondition.Name = "gbCondition";
            this.gbCondition.Size = new System.Drawing.Size(362, 58);
            this.gbCondition.TabIndex = 6;
            this.gbCondition.TabStop = false;
            this.gbCondition.Text = "Condition";
            // 
            // rbPunishment
            // 
            this.rbPunishment.AutoSize = true;
            this.rbPunishment.Location = new System.Drawing.Point(190, 19);
            this.rbPunishment.Name = "rbPunishment";
            this.rbPunishment.Size = new System.Drawing.Size(91, 19);
            this.rbPunishment.TabIndex = 1;
            this.rbPunishment.TabStop = true;
            this.rbPunishment.Text = "Punishment";
            this.rbPunishment.UseVisualStyleBackColor = true;
            this.rbPunishment.CheckedChanged += new System.EventHandler(this.rbPunishment_CheckedChanged);
            // 
            // rbReward
            // 
            this.rbReward.AutoSize = true;
            this.rbReward.Location = new System.Drawing.Point(78, 20);
            this.rbReward.Name = "rbReward";
            this.rbReward.Size = new System.Drawing.Size(68, 19);
            this.rbReward.TabIndex = 0;
            this.rbReward.TabStop = true;
            this.rbReward.Text = "Reward";
            this.rbReward.UseVisualStyleBackColor = true;
            this.rbReward.CheckedChanged += new System.EventHandler(this.rbReward_CheckedChanged);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(388, 553);
            this.ControlBox = false;
            this.Controls.Add(this.gbCondition);
            this.Controls.Add(this.gbWhiteList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbTopMost);
            this.Controls.Add(this.gbImagery);
            this.Controls.Add(this.gbSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.TopMost = true;
            this.gbSize.ResumeLayout(false);
            this.gbSize.PerformLayout();
            this.gbImagery.ResumeLayout(false);
            this.gbImagery.PerformLayout();
            this.gbWhiteList.ResumeLayout(false);
            this.gbWhiteList.PerformLayout();
            this.gbCondition.ResumeLayout(false);
            this.gbCondition.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSize;
        private System.Windows.Forms.RadioButton rbFullScreen;
        private System.Windows.Forms.RadioButton rbSizeSmall;
        private System.Windows.Forms.GroupBox gbImagery;
        private System.Windows.Forms.RadioButton rdbProgbar;
        private System.Windows.Forms.RadioButton rdbFlower;
        private System.Windows.Forms.CheckBox cbTopMost;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbWhiteList;
        private ManagedWinapi.Crosshair crosshair;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lbxWhiteList;
        private System.Windows.Forms.GroupBox gbCondition;
        private System.Windows.Forms.RadioButton rbPunishment;
        private System.Windows.Forms.RadioButton rbReward;
        
    }
}