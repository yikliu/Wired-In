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
/// <summary>
/// The View namespace.
/// </summary>
namespace WiredIn.Visualization.View
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using WiredIn.Globals;

    /// <summary>
    /// Class CustomView.
    /// </summary>
    public partial class CustomView : AbstractView
    {
        #region Fields

        /// <summary>
        /// The current identifier
        /// </summary>
        private int currentId = 1; //initially, set to the middle one.
        /// <summary>
        /// The image folder
        /// </summary>
        private string imageFolder;
        /// <summary>
        /// The images
        /// </summary>
        private Dictionary<int, Image> images;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomView"/> class.
        /// </summary>
        /// <returns></returns>
        public CustomView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomView"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public CustomView(string name)
            : this()
        {
            this.viewName = name;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public override int GetScore()
        {
            return -1;
        }

        /// <summary>
        /// Moves the view.
        /// </summary>
        /// <param name="goToGood">if set to <c>true</c> [go to good].</param>
        public override void MoveView(bool goToGood)
        {
            int newId = -1;

            if (goToGood)
            {
                newId = currentId + 1 > 2 ? 2 : currentId + 1;
            }
            else
            {
                newId = currentId - 1 < 0 ? 0 : currentId - 1;
            }

            if (newId != currentId && newId >= 0)
            {
                currentId = newId;
                pictureBox1.Image = images[currentId];
            }
        }

        /// <summary>
        /// Sets the size.
        /// </summary>
        /// <param name="s">The s.</param>
        public override void SetSize(Size s)
        {
            this.Size = s;
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        /// <exception cref="System.Exception">3 Images Only</exception>
        public override void SetUp()
        {
            imageFolder = Path.Combine(config.WiredInFolder, "visualizations\\" + viewName);
            if (Directory.Exists(imageFolder))
            {
                string[] files = Directory.GetFiles(imageFolder);
                int count = files.Length;
                if (count != 3)
                {
                    throw new Exception("3 Images Only");
                }

                //Load images into data structure
                images = new Dictionary<int, Image>();
                foreach (string file in files)
                {
                    Image img = Image.FromFile(file);
                    int id = Int32.Parse(Path.GetFileNameWithoutExtension(file).Substring(5));
                    images.Add(id, img);
                }

                //place image
                pictureBox1.Image = images[currentId];
            }
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        public override void TearDown()
        {
        }

        #endregion Methods
    }
}