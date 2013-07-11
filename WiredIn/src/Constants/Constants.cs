using System;
using System.IO;
using System.Collections.Generic;

namespace WiredIn.Constants
{
    public enum AppSize { Small, Medium, Full };
    
    public enum Visualization { ManyStepImages, Progressbar, Empty, Custom };
    
    public enum OperandCondition { reward, punish };

    public enum State { Good, Bad, Dormant };
    
    /// <summary>
    /// Global constants
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// use flower or other visual representation
        /// </summary>
        public static Visualization VIS_IMAGE = Visualization.ManyStepImages;

        public static string WIREDIN_FOLDER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "WiredIn");
        
        public static OperandCondition CONDITION = OperandCondition.reward;
        
        public const int DORMANT_INTERVAL_SECONDS = 20;
        
        public const int SLOW_UPDATE_RATE_MILLISECONDS = 450;
        
        public const int FAST_UPDATE_RATE_MILLISECONDS = 350;

        public const int STANDARD_STEPS = 1000;

        public static AppSize APP_SIZE = AppSize.Small;   
     
        public static bool LabelImageNum = false;

        public static bool TOPMOST = false;        
     
        public static List<String> WHITE_WIN = new List<String>() { "word" };

        public static List<String> WHITE_PROC = new List<String>() { "winword" };

        public static int SHRINK_FACTOR = 5; // small screen is shrunk to factor of screen size;

        public static bool ENABLE_SQL_LOGGING = false;
    }
}
