using System;
using System.Collections.Generic;
using System.Linq;
using WiredIn.Constants;

namespace WiredIn.Analyzer
{
    /// <summary>
    /// Judge determines whether Subject is on or off task
    /// </summary>
    public class Judge
    {
        private SingletonConstant constant = SingletonConstant.GetSingletonConstant();
        
        public bool CheckOnTask(WindowInfo newWinInfo)
        {
            if (CheckKeyWords(newWinInfo.WinTitle))
            {               
                return true;
            }                      
            return false;
        }

        private bool CheckWindows(WindowInfo curWin) 
        {
            return false;
        }

        private bool CheckKeyWords(String title)
        {
            title = title.ToLowerInvariant();
            String[] words = title.Split(' ');
            
            IEnumerable<String> both = constant.WhiteListKeyWords.Intersect(words);           

            int count = 0;
            foreach(String s in both){
                count++;
                return true;
            }
            return false;
        }
    }

}
