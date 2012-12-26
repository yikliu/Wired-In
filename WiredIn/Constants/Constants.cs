using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiredIn.Constants
{
    public enum app_size { small, medium, full };
    public enum imagery { flower, progressbar};
    public enum operant_condition{reward, punish};
    public static class Config
    {
        public const int DORMANT_INTERVAL_SECONDS = 30;
        public const int SLOW_UPDATE_RATE_MILLISECONDS = 1000;
        public const int FAST_UPDATE_RATE_MILLISECONDS = 350;

        public static app_size APP_SIZE = app_size.medium;        
        public static imagery VIS_IMAGE = imagery.flower;

        public static bool TOPMOST = false;

        public static operant_condition OPERAND_CONDITION = operant_condition.reward;

        public static List<String> WHITE_WIN = new List<String>() { "1" ,"2"};

        public static List<String> WHITE_PROC = new List<String>() { "WINWORD" };
    }
}
