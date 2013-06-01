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
            this.rbEmpty = new System.Windows.Forms.RadioButton();
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
            this.gbSize.Location = new System.Drawing.Point(12, 12);
            this.gbSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbSize.Name = "gbSize";
            this.gbSize.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbSize.Size = new System.Drawing.Size(493, 78);
            this.gbSize.TabIndex = 1;
            this.gbSize.TabStop = false;
            this.gbSize.Text = "Size";
            // 
            // rbFullScreen
            // 
            this.rbFullScreen.AutoSize = true;
            this.rbFullScreen.Location = new System.Drawing.Point(256, 33);
            this.rbFullScreen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbFullScreen.Name = "rbFullScreen";
            this.rbFullScreen.Size = new System.Drawing.Size(100, 21);
            this.rbFullScreen.TabIndex = 2;
            this.rbFullScreen.Text = "Full Screen";
            this.rbFullScreen.UseVisualStyleBackColor = true;
            this.rbFullScreen.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rbSizeSmall
            // 
            this.rbSizeSmall.AutoSize = true;
            this.rbSizeSmall.Location = new System.Drawing.Point(107, 33);
            this.rbSizeSmall.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbSizeSmall.Name = "rbSizeSmall";
            this.rbSizeSmall.Size = new System.Drawing.Size(63, 21);
            this.rbSizeSmall.TabIndex = 0;
            this.rbSizeSmall.Text = "Small";
            this.rbSizeSmall.UseVisualStyleBackColor = true;
            this.rbSizeSmall.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // gbImagery
            // 
            this.gbImagery.Controls.Add(this.rbEmpty);
            this.gbImagery.Controls.Add(this.rdbProgbar);
            this.gbImagery.Controls.Add(this.rdbFlower);
            this.gbImagery.Location = new System.Drawing.Point(12, 95);
            this.gbImagery.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbImagery.Name = "gbImagery";
            this.gbImagery.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbImagery.Size = new System.Drawing.Size(493, 78);
            this.gbImagery.TabIndex = 2;
            this.gbImagery.TabStop = false;
            this.gbImagery.Text = "Imagery";
            // 
            // rdbProgbar
            // 
            this.rdbProgbar.AutoSize = true;
            this.rdbProgbar.Location = new System.Drawing.Point(172, 33);
            this.rdbProgbar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdbProgbar.Name = "rdbProgbar";
            this.rdbProgbar.Size = new System.Drawing.Size(112, 21);
            this.rdbProgbar.TabIndex = 1;
            this.rdbProgbar.TabStop = true;
            this.rdbProgbar.Text = "Progress Bar";
            this.rdbProgbar.UseVisualStyleBackColor = true;
            this.rdbProgbar.CheckedChanged += new System.EventHandler(this.rdbProgbar_CheckedChanged);
            // 
            // rdbFlower
            // 
            this.rdbFlower.AutoSize = true;
            this.rdbFlower.Location = new System.Drawing.Point(44, 33);
            this.rdbFlower.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdbFlower.Name = "rdbFlower";
            this.rdbFlower.Size = new System.Drawing.Size(70, 21);
            this.rdbFlower.TabIndex = 0;
            this.rdbFlower.Text = "Flower";
            this.rdbFlower.UseVisualStyleBackColor = true;
            this.rdbFlower.CheckedChanged += new System.EventHandler(this.rdbFlower_CheckedChanged);
            // 
            // cbTopMost
            // 
            this.cbTopMost.AutoSize = true;
            this.cbTopMost.Location = new System.Drawing.Point(13, 178);
            this.cbTopMost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbTopMost.Name = "cbTopMost";
            this.cbTopMost.Size = new System.Drawing.Size(89, 21);
            this.cbTopMost.TabIndex = 3;
            this.cbTopMost.Text = "Top Most";
            this.cbTopMost.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(107, 253);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(261, 49);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbWhiteList
            // 
            this.gbWhiteList.Controls.Add(this.lbxWhiteList);
            this.gbWhiteList.Controls.Add(this.btnDelete);
            this.gbWhiteList.Controls.Add(this.btnCancel);
            this.gbWhiteList.Controls.Add(this.btnAdd);
            this.gbWhiteList.Controls.Add(this.lbl_title);
            this.gbWhiteList.Controls.Add(this.crosshair);
            this.gbWhiteList.Location = new System.Drawing.Point(15, 287);
            this.gbWhiteList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbWhiteList.Name = "gbWhiteList";
            this.gbWhiteList.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbWhiteList.Size = new System.Drawing.Size(492, 330);
            this.gbWhiteList.TabIndex = 5;
            this.gbWhiteList.TabStop = false;
            this.gbWhiteList.Text = "White List";
            // 
            // lbxWhiteList
            // 
            this.lbxWhiteList.FormattingEnabled = true;
            this.lbxWhiteList.ItemHeight = 16;
            this.lbxWhiteList.Location = new System.Drawing.Point(1, 81);
            this.lbxWhiteList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbxWhiteList.Name = "lbxWhiteList";
            this.lbxWhiteList.Size = new System.Drawing.Size(484, 132);
            this.lbxWhiteList.TabIndex = 5;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.113208F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(107, 52);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.113208F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(12, 52);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Location = new System.Drawing.Point(8, 18);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(103, 17);
            this.lbl_title.TabIndex = 2;
            this.lbl_title.Text = "<Un-specified>";
            // 
            // crosshair
            // 
            this.crosshair.Location = new System.Drawing.Point(417, 37);
            this.crosshair.Margin = new System.Windows.Forms.Padding(5);
            this.crosshair.Name = "crosshair";
            this.crosshair.Size = new System.Drawing.Size(52, 38);
            this.crosshair.TabIndex = 1;
            this.toolTip1.SetToolTip(this.crosshair, "Drag crosshair onto the window to add ");
            this.crosshair.CrosshairDragged += new System.EventHandler(this.crosshair1_CrosshairDragged);
            this.crosshair.CrosshairDragging += new System.EventHandler(this.crosshair1_CrosshairDragging);
            // 
            // gbCondition
            // 
            this.gbCondition.Controls.Add(this.rbPunishment);
            this.gbCondition.Controls.Add(this.rbReward);
            this.gbCondition.Location = new System.Drawing.Point(15, 209);
            this.gbCondition.Margin = new System.Windows.Forms.Padding(4);
            this.gbCondition.Name = "gbCondition";
            this.gbCondition.Padding = new System.Windows.Forms.Padding(4);
            this.gbCondition.Size = new System.Drawing.Size(483, 71);
            this.gbCondition.TabIndex = 6;
            this.gbCondition.TabStop = false;
            this.gbCondition.Text = "Condition";
            // 
            // rbPunishment
            // 
            this.rbPunishment.AutoSize = true;
            this.rbPunishment.Location = new System.Drawing.Point(253, 23);
            this.rbPunishment.Margin = new System.Windows.Forms.Padding(4);
            this.rbPunishment.Name = "rbPunishment";
            this.rbPunishment.Size = new System.Drawing.Size(103, 21);
            this.rbPunishment.TabIndex = 1;
            this.rbPunishment.TabStop = true;
            this.rbPunishment.Text = "Punishment";
            this.rbPunishment.UseVisualStyleBackColor = true;
            this.rbPunishment.CheckedChanged += new System.EventHandler(this.rbPunishment_CheckedChanged);
            // 
            // rbReward
            // 
            this.rbReward.AutoSize = true;
            this.rbReward.Location = new System.Drawing.Point(104, 25);
            this.rbReward.Margin = new System.Windows.Forms.Padding(4);
            this.rbReward.Name = "rbReward";
            this.rbReward.Size = new System.Drawing.Size(77, 21);
            this.rbReward.TabIndex = 0;
            this.rbReward.TabStop = true;
            this.rbReward.Text = "Reward";
            this.rbReward.UseVisualStyleBackColor = true;
            this.rbReward.CheckedChanged += new System.EventHandler(this.rbReward_CheckedChanged);
            // 
            // rbEmpty
            // 
            this.rbEmpty.AutoSize = true;
            this.rbEmpty.Location = new System.Drawing.Point(323, 32);
            this.rbEmpty.Name = "rbEmpty";
            this.rbEmpty.Size = new System.Drawing.Size(68, 21);
            this.rbEmpty.TabIndex = 2;
            this.rbEmpty.TabStop = true;
            this.rbEmpty.Text = "Empty";
            this.rbEmpty.UseVisualStyleBackColor = true;
            this.rbEmpty.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged_1);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(517, 681);
            this.ControlBox = false;
            this.Controls.Add(this.gbCondition);
            this.Controls.Add(this.gbWhiteList);
            this.Controls.Add(this.cbTopMost);
            this.Controls.Add(this.gbImagery);
            this.Controls.Add(this.gbSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.RadioButton rbEmpty;
        
    }
}