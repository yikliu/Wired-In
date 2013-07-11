// ***********************************************************************
// Assembly         : WiredIn
// Author           : yikliu
// Created          : 06-28-2013
//
// Last Modified By : yikliu
// Last Modified On : 10-15-2012
// ***********************************************************************
// <copyright file="WindowInfo.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ManagedWinapi.Windows;

namespace WiredIn.Windows
{
    /// <summary>
    /// Class WindowInfo
    /// </summary>
    class WindowInfo
    {
        private String procName;
        private int procID;
        private String winTitle;
        private IntPtr winHandle;

        public String ProcName
        {
            get 
            {
                return this.procName;
            }
        }

        public String WinTitle
        {
            get
            {
                return this.winTitle;
            }

        }

        public WindowInfo()
        {
        }
        
        public WindowInfo(SystemWindow win)
        {
            this.update(win);
        }

        public bool Equals(WindowInfo info){
            return this.winHandle == info.winHandle;
        }

        public bool belongToSameProcess(WindowInfo info)
        {
            return this.procID == info.procID;
        }

        public bool belongToSameProcess(SystemWindow win)
        {
            return this.procID == win.Process.Id;
        }

        public void update(SystemWindow win)
        {
            this.procID = win.Process.Id;
            this.procName = win.Process.ProcessName;
            this.winTitle = win.Title;
            this.winHandle = win.HWnd;
        }
    }
}
