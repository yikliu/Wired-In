using System;
using System.IO;
using System.Text;
using WiredIn.Constants;
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
        private String fullPath;

        private StreamWriter writer;
        private SqlWriter sqlWriter;

        public Logger()
        {
            bool sql = SingletonConstant.GetSingletonConstant().EnableDBLogging;
            
            currentDirectoryString = System.Windows.Forms.Application.StartupPath;
            path = currentDirectoryString + "\\..\\log\\";

            if (sql)
            {
                sqlWriter = new SqlWriter();
            }           

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

            this.fullPath = Path.Combine(path, file_name);

            if (sql)
            {
                sqlWriter.Prepare(); //prepare the sql statements                  
            }              
        }

        public void OpenFile()
        {
            SingletonConstant constants = SingletonConstant.GetSingletonConstant();

            StringBuilder sb = new StringBuilder();
            if (constants.ActiveView == Visualization.ManyStepImages)
            {
                sb.Append("Flower");
            }
            else if (constants.ActiveView == Visualization.Progressbar)
            {
                sb.Append("Progressbar");
            }else
            {
                sb.Append("Empty");                
            }

            if (constants.Condition == OperandCondition.punish)
            {
                sb.Append("+Punish");
            }
            else if (constants.Condition == OperandCondition.reward)
            {
                sb.Append("+Reward");
            }
            writer = new StreamWriter(fullPath, true);
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
