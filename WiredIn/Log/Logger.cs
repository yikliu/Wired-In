using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using WiredIn.UserActivity;


namespace WiredIn.Log
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

        private SqlWriter sqlWriter;

        public Logger()
        {
            currentDirectoryString = System.Windows.Forms.Application.StartupPath;
            path = currentDirectoryString + "\\..\\log\\";
            sqlWriter = new SqlWriter();

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

            sqlWriter.Prepare(); //prepare the sql statements                     
        }

        public void OpenFile() {            
            StringBuilder sb = new StringBuilder();
            if (Constants.Config.VIS_IMAGE == Constants.imagery.flower)
            {
                sb.Append("Flower");
            }
            else if (Constants.Config.VIS_IMAGE == Constants.imagery.progressbar)
            {
                sb.Append("Progressbar");
            }else
            {
                sb.Append("Empty");                
            }

            if (Constants.Config.OPERAND_CONDITION == Constants.operand_condition.punish)
            {
                sb.Append("+Punish");
            }
            else if (Constants.Config.OPERAND_CONDITION == Constants.operand_condition.reward)
            {
                sb.Append("+Reward");
            }
            writer = new StreamWriter(full_path, true);
            writer.WriteLine(sb.ToString());
        }

        public void CloseFile() {
            if (writer != null)
            {
                writer.Flush();
                writer.Close();
            }

                     
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
            sb.Append(ac.getScore());
            sb.Append(", ");
            sb.Append(ac.StateString);
            sb.Append(",");
            
            sb.Append(ac.What());
            sb.Append(ac.getStats());
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
