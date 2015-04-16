#region Header

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

#endregion Header

using System.Diagnostics;

namespace WiredIn.UI
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Forms;

    using WiredIn.Globals;

    /// <summary>
    /// Class VisualizationOrganizer
    /// </summary>
    public partial class VisualizationOrganizer : MetroFramework.Forms.MetroForm
    {
        #region Fields

        private ConfigVariables constants = ConfigVariables.GetConfigVariables();

        /// <summary>
        /// The image_set
        /// </summary>
        private int[] image_set = {0,0,0};

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualizationOrganizer"/> class.
        /// </summary>
        /// <returns></returns>
        public VisualizationOrganizer() 
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Deserializes the specified filename.
        /// </summary>
        /// <param viewName="filename">The filename.</param>
        public static void Deserialize(String filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    Stream stream = File.Open(filename, FileMode.Open);
                    var bformater = new BinaryFormatter();

                    stream.Close();
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Serializes the specified filename.
        /// </summary>
        /// <param viewName="filename">The filename.</param>
        public static void Serialize(String filename)
        {
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param viewName="sender">The source of the event.</param>
        /// <param viewName="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
            string folder = Path.Combine(constants.WiredInFolder, "images");
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

        

        /// <summary>
        /// Handles the Click event of the picBox control.
        /// </summary>
        /// <param viewName="sender">The source of the event.</param>
        /// <param viewName="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        #endregion Methods
    }
}