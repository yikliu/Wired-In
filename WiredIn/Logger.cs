using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using WiredIn.UserActivity;

namespace WiredIn
{   
    /// <summary>
    /// Logger writes user activity + time to log file
    /// </summary>
    public class Logger
    {
        private String currentDirectoryString;
        private String path;
        
        private String text;
        private String full_path;

        private StreamWriter writer;

        public Logger()
        {
            currentDirectoryString = System.Windows.Forms.Application.StartupPath;
            path = currentDirectoryString + "\\log\\";

            DirectoryInfo di = new DirectoryInfo(path);

            if (!di.Exists)
                di.Create();
            
            DateTime now = DateTime.Now;
            String file_name = Environment.MachineName + "-" 
                + now.Year.ToString() + "-" 
                + now.Month.ToString() + "-" 
                + now.Day.ToString() + "-" 
                + now.Hour.ToString() + "-" 
                + now.Minute.ToString() + ".csv";

            this.full_path = Path.Combine(path, file_name);
        }

        public void OpenFile() {
            writer = new StreamWriter(full_path,true);       
        }

        public void CloseFile() {
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// Format each line of log
        /// </summary>
        /// <param name="ac">Activity instance that is being logged</param>
        /// <returns></returns>
        private String FormatLog(Activity ac)
        {
            StringBuilder sb = new StringBuilder();
            DateTime dt = ac.When();
            sb.Append(dt.ToShortDateString() + ", "+dt.ToLongTimeString());
            sb.Append(", ");
            sb.Append(ac.What());            
            sb.Append(ac.getPreviousACDuration());
            return sb.ToString();
        }

        /// <summary>
        /// Log an activity
        /// </summary>
        /// <param name="ac">the activity instance that is being logged</param>
        public void Log(Activity ac)
        {
            if (writer == null)
                OpenFile();
            
            text = this.FormatLog(ac);

            try
            {
                this.writer.WriteLine(text);                  
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }            
        }

    }
}
