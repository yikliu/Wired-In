using System;
using System.Collections.Generic;

namespace WiredIn.Constants
{
    class SingletonConstant
    {
        private static SingletonConstant _instance;

        protected SingletonConstant()  {  }

        public static SingletonConstant GetSingletonConstant()
        {
            if (_instance == null)
            {
                _instance = new SingletonConstant();
            }

            return _instance;
        }

        public Visualization ActiveView = Visualization.ManyStepImages;

        public string ActiveVisualizationName = "rose";

        public string WiredInFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "WiredIn");

        public OperandCondition Condition = OperandCondition.reward;

        public readonly int DormantIntervalInSeconds = 20;

        public readonly int SlowUpdateRateInMilliSeconds = 450;

        public readonly int FastUpdateRateInMilliSeconds = 350;

        public readonly int StandardSteps = 1000;

        public ApplicationSize WindowSize = ApplicationSize.Small;

        public bool LabelImageNum = true;

        public bool TopMost = false;

        public List<string> WhiteListWinTitles = new List<string>() { };
        
        public List<string> WhiteListKeyWords = new List<string>() { };

        public readonly int ShrinkFactor = 5; // small screen is shrunk to factor of screen size;

        public bool EnableDBLogging = false;
    }
}
