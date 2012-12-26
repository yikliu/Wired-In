using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using WiredIn.UserActivity;

namespace WiredIn
{
    class Logger
    {
        private ObservableCollection<Activity> activity_collection;
        private String currentDirectoryString;
        private String path;
        
        private String text;
        private Activity ac;
        private String file_name;

        public Logger(ObservableCollection<Activity> queue)
        {
            activity_collection = queue;

            currentDirectoryString = System.Windows.Forms.Application.StartupPath;
            path = currentDirectoryString + "\\log\\";

            DirectoryInfo di = new DirectoryInfo(path);

            if (di.Exists == false)
                di.Create();
            
            DateTime now = DateTime.Now;
            file_name = Environment.MachineName + "-" 
                + now.Year.ToString() + "-" 
                + now.Month.ToString() + "-" 
                + now.Day.ToString() + "-" 
                + now.Hour.ToString() + "-" 
                + now.Minute.ToString() + ".txt";
        }

        public void DequeueActivity()
        {
            if (activity_collection.Count <= 0)
                return;

            ac = activity_collection[0];
            text = ac.When() + "," + ac.What();
            String file = Path.Combine(path, file_name);
           
            try
            {
               using (StreamWriter writer = new StreamWriter(file,true))
               {
                   writer.WriteLine(text);                  
               }                               
               lock(this)
               {
                    activity_collection.RemoveAt(0);
               }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            
        }

    }
}
