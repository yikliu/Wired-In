
namespace WiredIn
{
    partial class VisualizationOrganizer
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imagePanel = new System.Windows.Forms.Panel();
            this.pBox_0 = new System.Windows.Forms.PictureBox();
            this.spLine = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pBox_1 = new System.Windows.Forms.PictureBox();
            this.pBox_2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbox_name = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.imagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBox_0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox_2)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Multiselect = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imagePanel
            // 
            this.imagePanel.Controls.Add(this.pBox_2);
            this.imagePanel.Controls.Add(this.pBox_1);
            this.imagePanel.Controls.Add(this.pBox_0);
            this.imagePanel.Location = new System.Drawing.Point(25, 25);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(744, 217);
            this.imagePanel.TabIndex = 10;
            // 
            // pBox_0
            // 
            this.pBox_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pBox_0.InitialImage = global::WiredIn.Properties.Resources.placeholder;
            this.pBox_0.Location = new System.Drawing.Point(14, 12);
            this.pBox_0.Name = "pBox_0";
            this.pBox_0.Size = new System.Drawing.Size(220, 192);
            this.pBox_0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pBox_0.TabIndex = 0;
            this.pBox_0.TabStop = false;
            this.pBox_0.Click += new System.EventHandler(this.picBox_Click);
            // 
            // spLine
            // 
            this.spLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spLine.Location = new System.Drawing.Point(77, 256);
            this.spLine.Name = "spLine";
            this.spLine.Size = new System.Drawing.Size(633, 2);
            this.spLine.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "Bad";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(700, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 24);
            this.label2.TabIndex = 13;
            this.label2.Text = "Good";
            // 
            // pBox_1
            // 
            this.pBox_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pBox_1.InitialImage = global::WiredIn.Properties.Resources.placeholder;
            this.pBox_1.Location = new System.Drawing.Point(243, 12);
            this.pBox_1.Name = "pBox_1";
            this.pBox_1.Size = new System.Drawing.Size(241, 192);
            this.pBox_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pBox_1.TabIndex = 1;
            this.pBox_1.TabStop = false;
            this.pBox_1.Click += new System.EventHandler(this.picBox_Click);
            // 
            // pBox_2
            // 
            this.pBox_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pBox_2.ErrorImage = global::WiredIn.Properties.Resources.error;
            this.pBox_2.InitialImage = global::WiredIn.Properties.Resources.placeholder;
            this.pBox_2.Location = new System.Drawing.Point(490, 12);
            this.pBox_2.Name = "pBox_2";
            this.pBox_2.Size = new System.Drawing.Size(242, 192);
            this.pBox_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pBox_2.TabIndex = 2;
            this.pBox_2.TabStop = false;
            this.pBox_2.Click += new System.EventHandler(this.picBox_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(53, 293);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 29);
            this.label3.TabIndex = 14;
            this.label3.Text = "Name: ";
            // 
            // tbox_name
            // 
            this.tbox_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbox_name.Location = new System.Drawing.Point(150, 290);
            this.tbox_name.Name = "tbox_name";
            this.tbox_name.Size = new System.Drawing.Size(490, 35);
            this.tbox_name.TabIndex = 15;
            this.tbox_name.Text = "My Visualizer";
            this.tbox_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(646, 288);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 40);
            this.button1.TabIndex = 16;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // VisualizationOrganizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 360);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbox_name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.spLine);
            this.Controls.Add(this.imagePanel);
            this.Name = "VisualizationOrganizer";
            this.Text = "Make Your Own Visual Feedback";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageImport_FormClosing);
            this.imagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pBox_0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox_2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel imagePanel;
        private System.Windows.Forms.Label spLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pBox_0;
        private System.Windows.Forms.PictureBox pBox_2;
        private System.Windows.Forms.PictureBox pBox_1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbox_name;
        private System.Windows.Forms.Button button1;

    }
}