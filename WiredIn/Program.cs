namespace WiredIn
{
    using System;
    using System.Windows.Forms;
    
    static class Program
    {
        #region Methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Demo.MainForm());

            WiredIn.UI.Orientation orient = new WiredIn.UI.Orientation();
            if (orient.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new WiredIn.ShowVisualizations());
            }
            else
            {
                Application.Exit();
            }

        }

        #endregion Methods
    }
}