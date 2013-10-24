/**
 * WiredIn - Visual Reminder of Suspended Tasks
 *
 * The MIT License (MIT)
 * Copyright (c) 2012 Yikun Liu, https://github.com/yikliu
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in the
 * Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
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

/// <summary>
/// The UI namespace.
/// </summary>
namespace WiredIn.UI
{
    using System;
    using System.Collections.Generic;
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

        /// <summary>
        /// The constants
        /// </summary>
        private ConfigVariables constants = ConfigVariables.GetConfigVariables();

        /// <summary>
        /// The image_set
        /// </summary>
        private int[] image_set = {0,0,0};

        /// <summary>
        /// The selected
        /// </summary>
        private int selected = -1;

        /// <summary>
        /// The vis names
        /// </summary>
        private List<String> visNames;

        /// <summary>
        /// The wired_in_folder
        /// </summary>
        private String wired_in_folder;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualizationOrganizer" /> class.
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
        /// Serializes the specified filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public static void Serialize(String filename)
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

        /// <summary>
        /// Handles the FormClosing event of the ImageImport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void ImageImport_FormClosing(object sender, FormClosingEventArgs e)
        {
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

        #endregion Methods
    }
}