namespace WiredIn
{
    partial class Guide
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
            this.label1 = new System.Windows.Forms.Label();
            this.gBox_Vis = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gbWhiteList = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbxWhiteList = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lbl_title = new System.Windows.Forms.Label();
            this.crosshair = new ManagedWinapi.Crosshair();
            this.button3 = new System.Windows.Forms.Button();
            this.tableLayoutPanel = new WiredIn.DoubleBufferedTableLayoutPanel();
            this.gBox_Vis.SuspendLayout();
            this.gbWhiteList.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(191, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(442, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hi, Ready to start today\'s work? ";
            // 
            // gBox_Vis
            // 
            this.gBox_Vis.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gBox_Vis.Controls.Add(this.tableLayoutPanel);
            this.gBox_Vis.Location = new System.Drawing.Point(33, 82);
            this.gBox_Vis.Name = "gBox_Vis";
            this.gBox_Vis.Size = new System.Drawing.Size(793, 66);
            this.gBox_Vis.TabIndex = 2;
            this.gBox_Vis.TabStop = false;
            this.gBox_Vis.Text = "Choose Feedback";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(36, 151);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 33);
            this.button2.TabIndex = 4;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(191, 411);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(436, 88);
            this.button1.TabIndex = 3;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gbWhiteList
            // 
            this.gbWhiteList.Controls.Add(this.label2);
            this.gbWhiteList.Controls.Add(this.lbxWhiteList);
            this.gbWhiteList.Controls.Add(this.btnDelete);
            this.gbWhiteList.Controls.Add(this.btnAdd);
            this.gbWhiteList.Controls.Add(this.lbl_title);
            this.gbWhiteList.Controls.Add(this.crosshair);
            this.gbWhiteList.Location = new System.Drawing.Point(33, 189);
            this.gbWhiteList.Margin = new System.Windows.Forms.Padding(2);
            this.gbWhiteList.Name = "gbWhiteList";
            this.gbWhiteList.Padding = new System.Windows.Forms.Padding(2);
            this.gbWhiteList.Size = new System.Drawing.Size(793, 206);
            this.gbWhiteList.TabIndex = 6;
            this.gbWhiteList.TabStop = false;
            this.gbWhiteList.Text = "White List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(381, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Drag the Crosshair on the window you want to include in the white list";
            // 
            // lbxWhiteList
            // 
            this.lbxWhiteList.FormattingEnabled = true;
            this.lbxWhiteList.Location = new System.Drawing.Point(4, 32);
            this.lbxWhiteList.Margin = new System.Windows.Forms.Padding(2);
            this.lbxWhiteList.Name = "lbxWhiteList";
            this.lbxWhiteList.Size = new System.Drawing.Size(667, 173);
            this.lbxWhiteList.TabIndex = 5;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.113208F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(683, 69);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(104, 33);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.113208F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(683, 32);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(104, 33);
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
            this.crosshair.Location = new System.Drawing.Point(683, 114);
            this.crosshair.Margin = new System.Windows.Forms.Padding(10);
            this.crosshair.MinimumSize = new System.Drawing.Size(10, 10);
            this.crosshair.Name = "crosshair";
            this.crosshair.Size = new System.Drawing.Size(38, 39);
            this.crosshair.TabIndex = 1;
            this.crosshair.CrosshairDragged += new System.EventHandler(this.crosshair_CrosshairDragged);
            this.crosshair.CrosshairDragging += new System.EventHandler(this.crosshair_CrosshairDragging);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(157, 151);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 33);
            this.button3.TabIndex = 7;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(787, 47);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // Guide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 511);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.gbWhiteList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gBox_Vis);
            this.Controls.Add(this.label1);
            this.Name = "Guide";
            this.Text = "Wired In";
            this.gBox_Vis.ResumeLayout(false);
            this.gbWhiteList.ResumeLayout(false);
            this.gbWhiteList.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gBox_Vis;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox gbWhiteList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbxWhiteList;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lbl_title;
        private ManagedWinapi.Crosshair crosshair;
        private DoubleBufferedTableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button button3;
    }
}