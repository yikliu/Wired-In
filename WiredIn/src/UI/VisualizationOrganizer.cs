// ***********************************************************************
// Assembly         : WiredIn
// Author           : yikliu
// Created          : 06-28-2013
//
// Last Modified By : yikliu
// Last Modified On : 09-05-2013
// ***********************************************************************
// <copyright file="VisualizationOrganizer.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using WiredIn.Constants;

namespace WiredIn.UI
{
    /// <summary>
    /// Class VisualizationOrganizer
    /// </summary>
    public partial class VisualizationOrganizer : MetroFramework.Forms.MetroForm
    {

        /// <summary>
        /// The selected
        /// </summary>
        private int selected = -1;
        /// <summary>
        /// The image_set
        /// </summary>
        private int[] image_set = {0,0,0};

        /// <summary>
        /// The wired_in_folder
        /// </summary>
        private String wired_in_folder;
        /// <summary>
        /// The vis names
        /// </summary>
        private List<String> visNames;

        private SingletonConstant constants = SingletonConstant.GetSingletonConstant(); 

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualizationOrganizer"/> class.
        /// </summary>
        /// <returns></returns>
        public VisualizationOrganizer()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// Handles the Click event of the picBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void picBox_Click(object sender, EventArgs e)
        {
            PictureBox box = (PictureBox)sender;
            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                String fileName = openFileDialog.FileName;
                Image img = Image.FromFile(fileName);
                box.Image = img;
                int index = Int32.Parse(box.Name.Substring(5));
                image_set[index] = 1;
            }
            
        }

        /// <summary>
        /// Serializes the specified filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public static void Serialize(String filename)
        {
           
        }

        /// <summary>
        /// Deserializes the specified filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public static void Deserialize(String filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    Stream stream = File.Open(filename, FileMode.Open);
                    BinaryFormatter bformater = new BinaryFormatter();
                 
                    stream.Close();
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }                
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the ImageImport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void ImageImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                if (0 == image_set[i])
                {
                    MessageBox.Show("Image " + i + "is Not Set", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    return;
                }
            }
            string name = tbox_name.Text;
            string folder = Path.Combine(constants.WiredInFolder, "visualizations");
            DirectoryInfo dir = Directory.CreateDirectory(Path.Combine(folder, name));
            foreach (Control c in imagePanel.Controls)
            {
                Image img = ((PictureBox)c).Image;
                String fileName = c.Name;
                img.Save(Path.Combine(dir.FullName,fileName+".jpg"));
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }       
    }
}
