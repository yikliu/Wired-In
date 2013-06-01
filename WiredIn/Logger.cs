using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using WiredIn.UserActivity;
using MySql.Data.MySqlClient;

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
        
        private MySqlConnection conn = null;
        private MySqlCommand cmd = null;
        private MySqlParameter title;
        private MySqlParameter score;
        private MySqlParameter time;

        public Logger()
        {
            currentDirectoryString = System.Windows.Forms.Application.StartupPath;
            path = currentDirectoryString + "\\..\\log\\";

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

            try
            {
                string cs = @"server=54.214.0.224;userid=root;password=678jaesy;database=wiredin";
                conn = new MySqlConnection();
                conn.ConnectionString = cs;
                conn.Open();

                cmd = new MySqlCommand();
                cmd.Connection = conn;
                string SQL = "INSERT INTO windows(title,score,time) VALUES(@title,@score,@time)";
                cmd.CommandText = SQL;
                
                title = new MySqlParameter("@title", MySqlDbType.VarChar);
                cmd.Parameters.Add(title);
                
                score = new MySqlParameter("@score", MySqlDbType.Int16);
                cmd.Parameters.Add(score);

                time = new MySqlParameter("@time", MySqlDbType.VarChar);
                cmd.Parameters.Add(time);
                
                cmd.Prepare();   
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }            
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

            if (conn != null)
            {
                title.Value = "closing";
                score.Value = 0;
                time.Value = DateTime.Now.ToString();
                cmd.ExecuteNonQuery();
                conn.Close();
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
            if (ac is WindowChangeActivity)
            {
                try
                {
                    title.Value = ac.What();
                    score.Value = ac.getScore();
                    time.Value = ac.When();
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());
                }
            }            
        }
    }
}
