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
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Windows.Forms;

    using WiredIn.Globals;

    /// <summary>
    /// Class ManyStepsView
    /// </summary>
    class ManyStepsView : AbstractView
    {
        #region Fields

        /// <summary>
        /// The component size
        /// </summary>
        protected Size componentSize;
        /// <summary>
        /// The content
        /// </summary>
        protected Bitmap content;

        /// <summary>
        /// The aspect ratio
        /// </summary>
        private double aspectRatio = 0.0;
        /// <summary>
        /// The configuration
        /// </summary>
        private ConfigVariables config = ConfigVariables.GetConfigVariables();
        /// <summary>
        /// The current identifier
        /// </summary>
        int currentID = 0;
        /// <summary>
        /// The file location
        /// </summary>
        private string fileLocation;
        /// <summary>
        /// The number of pics
        /// </summary>
        int numOfPics = 0;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ManyStepsView"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ManyStepsView(string name)
        {
            this.viewName = name;
        }

        #endregion Constructors

        #region Delegates

        /// <summary>
        /// Delegate RefreshViewDelegate
        /// </summary>
        private delegate void RefreshViewDelegate();

        #endregion Delegates

        #region Methods

        /// <summary>
        /// Creates the fast bitmap.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <returns>Bitmap.</returns>
        public static Bitmap CreateFastBitmap(Image src)
        {
            Bitmap dest = new Bitmap(src.Width, src.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.DrawImage(src, 0, 0, src.Width, src.Height);
            }
            return dest;
        }

        /// <summary>
        /// Disposes the image.
        /// </summary>
        /// <param name="p">The p.</param>
        public void disposeImage(Bitmap p)
        {
            if (p != null)
            {
                p.Dispose();
            }
        }

        /// <summary>
        /// Gets the count of steps.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public override int GetCountOfSteps()
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(fileLocation);
            return dir.GetFiles().Length;
        }

        /// <summary>
        /// Gets the image by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Image.</returns>
        public Image getImageByID(int id)
        {
            string path = fileLocation + "\\"+id + ".jpg";
            Image img = Image.FromFile(path);
            this.aspectRatio = (double)img.Height / (double)img.Width;
            return img;
        }

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public override int GetScore()
        {
            return currentID;
        }

        /// <summary>
        /// Moves the view.
        /// </summary>
        /// <param name="goToGood">if set to <c>true</c> [go to good].</param>
        public override void MoveView(bool goToGood)
        {
            if (!goToGood && currentID > 0)
            {
                currentID--;
            }
            else if(goToGood && (currentID < numOfPics - 1)){
                currentID++;
            }
            RefreshView();
        }

        /// <summary>
        /// Refreshes the view.
        /// </summary>
        public void RefreshView()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new RefreshViewDelegate(RefreshView));
            }
            else
            {
                disposeImage(this.content);
                this.content = CreateFastBitmap(getImageByID(currentID));
                this.Refresh();
            }
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        /// <exception cref="System.Exception">viewName not set</exception>
        public override void SetUp()
        {
            try
            {
                if (null == viewName)
                {
                    throw new Exception("viewName not set");
                }
                else
                {
                    this.fileLocation = Path.Combine(config.WiredInFolder, "visualizations\\" + viewName);
                    numOfPics = GetCountOfSteps();
                    currentID = 2 * (numOfPics / 3);
                    RefreshView();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        public override void TearDown()
        {
            disposeImage(this.content);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(this.BackColor);

            if (this.content != null) //get the correct drawing coordinates, keep aspect ratio
            {
                int w = this.Size.Width;
                int h = (int)(this.Size.Width * this.aspectRatio);
                int start_h = (this.Size.Height - h) / 2;
                e.Graphics.DrawImage(this.content, 0, start_h, w, h);
            }

            String str;
            if (config.DebugImageNum)
            {
                str = "Pic:" + currentID + "(" + curState.ToString();
                if (config.Condition == Globals.OperandConditioning.Punish)
                    str += ", Punish)";
                else
                    str += ", reward)";

                using (Font myFont = new Font("Arial", 13))
                {
                    e.Graphics.DrawString(str, myFont, Brushes.Yellow, new Point(2, 2));
                }
            }
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // ManyStepsView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.Name = "ManyStepsView";
            this.ResumeLayout(false);
        }

        #endregion Methods
    }
}