using System;
using System.Collections.Generic;

namespace WiredIn.Constants
{
    public enum app_size { small, medium, full };
    public enum imagery { flower, progressbar, empty };
    public enum operand_condition{ reward, punish };
    
    /// <summary>
    /// Global constants
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// use flower or other visual representation
        /// </summary>
        public static imagery VIS_IMAGE = imagery.flower;
        
        public static operand_condition OPERAND_CONDITION = operand_condition.punish;
        
        public const int DORMANT_INTERVAL_SECONDS = 20;
        
        public const int SLOW_UPDATE_RATE_MILLISECONDS = 450;
        
        public const int FAST_UPDATE_RATE_MILLISECONDS = 350;

        public static app_size APP_SIZE = app_size.full;   
     
        public static bool LabelImageNum = false;

        public static bool TOPMOST = false;        
     
        public static List<String> WHITE_WIN = new List<String>() { "word" };

        public static List<String> WHITE_PROC = new List<String>() { "winword" };

        public static int SHRINK_FACTOR = 5; // small screen is shrunk to factor of screen size;
    }
}
