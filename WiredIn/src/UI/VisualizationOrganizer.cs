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

namespace WiredIn
{
    public partial class VisualizationOrganizer : Form
    {        
      
        private int selected = -1;
        private int[] image_set = {0,0,0};

        private String wired_in_folder;
        private List<String> visNames;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public VisualizationOrganizer()
        {
            InitializeComponent();           
            
        }

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
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="ser"></param>
        /// <returns></returns>
        public static void Serialize(String filename)
        {
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
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

        private void ImageImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

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
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "WiredIn");
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
