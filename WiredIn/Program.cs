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
            
            var orient = new UI.Orientation();
            if (orient.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new ShowVisualizations());
            }
            else
            {
                Application.Exit();
            }
        }

        #endregion Methods
    }
}