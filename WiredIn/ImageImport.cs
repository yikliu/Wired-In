using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Filmstrip;

namespace WiredIn
{
    public partial class ImageImport : Form
    {
        public static List<FilmStripSerie> _allSeries = null;
        public static String ObjectFile = "my_visualizations.dat";
        
        private double scaleRatio;
        // The thumbnail size and separator are fixed values...
        internal const int thumbnailSeperatorSize = 20;
        internal const int thumbnailPanelWidth = 75;
        internal const int thumbnailPanelMargin = 10;
        internal const int thumbnailImageMargin = 5;

        private int currentSelectedId = -1;

        public ImageImport()
        {
            InitializeComponent();           
            LoadVisualizations();           
        }

        public void LoadVisualizations()
        {
            _allSeries = new List<FilmStripSerie>();
            //Read serialize file first
            ImageImport.Deserialize(ObjectFile);
            populateThumbNails();
        }

        public void populateThumbNails() 
        {
            this.panelVis.Controls.Clear();
            if (_allSeries.Count > 0)
            {
                int i = 0;
                foreach (FilmStripSerie s in _allSeries)
                {
                    PushThumbnail(i);
                    i++;
                }
                btnDelete.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
            }
            PushThumbnail(-1);
        }

        private Image ScaleImage(Image image, int width, int height)
        {
            Image returnImage = null;
            // Only bother if the supplied dimensions differ from the actual image dimensions
            if ((width != image.Width) || (height != image.Height))
            {
                // Scaling ratios - assume 1 in case either width or height is 0.
                double widthRatio = 1;
                double heightRatio = 1;
                if (image.Width > 0)
                {
                    widthRatio = (double)width / (double)image.Width;
                }
                if (image.Height > 0)
                {
                    heightRatio = (double)height / (double)image.Height;
                }

                // Scale according to the correct ratio
                if (widthRatio < heightRatio)
                {
                    returnImage = image.GetThumbnailImage((int)(image.Width * widthRatio), (int)(image.Height * widthRatio), ThumbnailCallback, IntPtr.Zero);
                    scaleRatio = widthRatio;
                }
                else
                {
                    returnImage = image.GetThumbnailImage((int)(image.Width * heightRatio), (int)(image.Height * heightRatio), ThumbnailCallback, IntPtr.Zero);
                    scaleRatio = heightRatio;
                }
            }
            else
            {
                returnImage = image;
                scaleRatio = 1.0;
            }
            return returnImage;
        }

        /// <summary>
        /// Callback function required for the use of GetThumbnailImage function
        /// </summary>
        /// <returns></returns>
        private bool ThumbnailCallback()
        {
            // Nothing to do here.
            return false;
        }

        private void btnAddImages_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                FilmStripSerie aSerie = new FilmStripSerie("undefined");
                foreach (String file in openFileDialog.FileNames)
                {
                    FilmstripImageRef image_ref = new FilmstripImageRef(file, System.IO.Path.GetFileName(file));
                    aSerie.AddImage(image_ref);                  
                }
                _allSeries.Add(aSerie);
                populateThumbNails();
                currentSelectedId = _allSeries.Count - 1;
                filmstripControl.SetFilmSerie(aSerie);
            }
        }
                
        public static void Serialize(String filename, List<FilmStripSerie> ser)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, ser);
            stream.Close();
        }

        public static void Deserialize(String filename)
        {
            if (File.Exists(filename))
            {
                Stream stream = File.Open(filename, FileMode.Open);
                BinaryFormatter bformater = new BinaryFormatter();
                ImageImport._allSeries = (List<FilmStripSerie>)bformater.Deserialize(stream);
                stream.Close();
            }
        }

        private void PushThumbnail(int index)
        {            
            // We leave a 20 pixel gap in between each one, and make them just smaller than the container's height
            int nThumbnailTop = panelVis.Top;
            int nThumbnailHeight = panelVis.Height - thumbnailPanelMargin;
            // Work out how many we can have and get the modulus, so we can center all the controls
            //int margin = (panelThumbnails.Width % (thumbnailPanelWidth + thumbnailSeperatorSize)) / 2;
            int margin = thumbnailSeperatorSize;

            // Create the panel
            Panel newPanel = new Panel();
            if (index == -1)
            {
                newPanel.Name = "Add Button";
            }
            else
            {
                newPanel.Name = "PanelVis" + index.ToString();
            }
            newPanel.Top = nThumbnailTop;
            newPanel.Height = nThumbnailHeight;
            newPanel.Width = thumbnailPanelWidth;
            newPanel.Left = margin;
            
            // Create the Button
            Button newButton = new Button();            
            if (index == -1)
            {
                newButton.Name = "Add Button";
            }
            else
            {
                newButton.Name = "PanelVis" + index.ToString();
            }
            // Make it 5 pixels in from it's Panel parent
            newButton.Top = thumbnailImageMargin;
            newButton.Height = nThumbnailHeight - thumbnailPanelMargin;
            newButton.Width = thumbnailPanelWidth - thumbnailPanelMargin;
            newButton.Left = thumbnailImageMargin;
            
            // Initially invisible (it has no image at the moment)
            //newButton.Visible = false;
            newButton.FlatStyle = FlatStyle.Flat;
            newButton.FlatAppearance.BorderSize = 0;
            newButton.Tag = index;
            
            if (index == -1) //the add button
            {
                newButton.Click += new EventHandler(btnAddImages_Click);
                newButton.Image = ScaleImage(global::WiredIn.Properties.Resources.plus_button, panelVis.Height, panelVis.Height);
            }
            else
            {
                newButton.Click += new EventHandler(Thumbnail_Click);
                Image icon;
                try
                {
                    icon = Image.FromFile(_allSeries[index].getRepresentativeImage().Location);
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    icon = new Bitmap(WiredIn.Properties.Resources.error);
                }
                newButton.Image = ScaleImage(icon, panelVis.Width, panelVis.Height);
            }
            
            // Add it to the parent panel
            newPanel.Controls.Add(newButton);           
            panelVis.Controls.Add(newPanel);                
        }

        /// <summary>
        /// Handler for when a thumbnail image is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Thumbnail_Click(object sender, EventArgs e)
        {
            // Get the image id off the control
            Button boxSender = sender as Button;
            // We populated this so the image id should be fine... but just in case.
            try
            {
                // Get the ID
                int serie_id = Convert.ToInt32(boxSender.Tag);
                if (serie_id == currentSelectedId)
                {
                    return;
                }                
                // Select this one
                FilmStripSerie serie = _allSeries[serie_id];
                filmstripControl.SetFilmSerie(serie);
                currentSelectedId = serie_id;               
            }
            catch (InvalidCastException)
            {
                // Should never get in here... but just in case
                MessageBox.Show("Error", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImageImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            ImageImport.Serialize(ObjectFile,ImageImport._allSeries);
        }

        private void btnLoadVis_Click(object sender, EventArgs e)
        {
            ImageImport.Deserialize(ObjectFile);
        }

        private void btnSaveVis_Click(object sender, EventArgs e)
        {
            ImageImport.Serialize(ObjectFile, ImageImport._allSeries);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentSelectedId > -1)
            {
                _allSeries.RemoveAt(currentSelectedId);
                if (_allSeries.Count > 0)
                {
                    FilmStripSerie ser = _allSeries[0];
                    filmstripControl.SetFilmSerie(ser);
                    currentSelectedId = 0;
                }
                else
                {
                    filmstripControl.RemoveFilmstripSerie();
                    currentSelectedId = -1;
                }
                populateThumbNails();
            }
        }
               
    }
}
