using Filmstrip;
namespace WiredIn
{
    partial class ImageImport
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelVis = new System.Windows.Forms.FlowLayoutPanel();
            this.filmstripControl = new Filmstrip.FilmstripControl();
            this.btnLoadVis = new System.Windows.Forms.Button();
            this.btnSaveVis = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Multiselect = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelVis);
            this.groupBox1.Location = new System.Drawing.Point(25, 35);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(730, 103);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Visualizations";
            // 
            // panelVis
            // 
            this.panelVis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panelVis.Location = new System.Drawing.Point(11, 18);
            this.panelVis.Name = "panelVis";
            this.panelVis.Size = new System.Drawing.Size(714, 80);
            this.panelVis.TabIndex = 1;
            this.panelVis.TabStop = true;
            // 
            // filmstripControl
            // 
            this.filmstripControl.BackColor = System.Drawing.SystemColors.Control;
            this.filmstripControl.Location = new System.Drawing.Point(25, 196);
            this.filmstripControl.Name = "filmstripControl";
            this.filmstripControl.Size = new System.Drawing.Size(730, 323);
            this.filmstripControl.TabIndex = 6;
            // 
            // btnLoadVis
            // 
            this.btnLoadVis.Location = new System.Drawing.Point(36, 144);
            this.btnLoadVis.Name = "btnLoadVis";
            this.btnLoadVis.Size = new System.Drawing.Size(136, 40);
            this.btnLoadVis.TabIndex = 7;
            this.btnLoadVis.Text = "Load Visualizations";
            this.btnLoadVis.UseVisualStyleBackColor = true;
            this.btnLoadVis.Click += new System.EventHandler(this.btnLoadVis_Click);
            // 
            // btnSaveVis
            // 
            this.btnSaveVis.Location = new System.Drawing.Point(178, 144);
            this.btnSaveVis.Name = "btnSaveVis";
            this.btnSaveVis.Size = new System.Drawing.Size(136, 40);
            this.btnSaveVis.TabIndex = 8;
            this.btnSaveVis.Text = "Save Visualizations";
            this.btnSaveVis.UseVisualStyleBackColor = true;
            this.btnSaveVis.Click += new System.EventHandler(this.btnSaveVis_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(320, 144);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(136, 40);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ImageImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 537);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSaveVis);
            this.Controls.Add(this.btnLoadVis);
            this.Controls.Add(this.filmstripControl);
            this.Controls.Add(this.groupBox1);
            this.Name = "ImageImport";
            this.Text = "ImageImport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageImport_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private FilmstripControl filmstripControl;

        private System.Windows.Forms.FlowLayoutPanel panelVis;
        private System.Windows.Forms.Button btnLoadVis;
        private System.Windows.Forms.Button btnSaveVis;
        private System.Windows.Forms.Button btnDelete;

    }
}