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

namespace WiredIn.Analyzer
{
    /// <summary>
    /// Class WindowInfo
    /// </summary>
    public class WindowInfo
    {
        private String _procName;
        private int _procID;
        private String _winTitle;
        private IntPtr _winHandle;

        public String ProcName
        {
            get 
            {
                return this._procName;
            }
        }

        public String WinTitle
        {
            get
            {
                return this._winTitle;
            }

        }

        public WindowInfo()
        {
        }
        
        public WindowInfo(SystemWindow win)
        {
            this.update(win);
        }

        public bool Equals(WindowInfo info)
        {
            return this._winHandle == info._winHandle;
        }

        public bool belongToSameProcess(WindowInfo info)
        {
            return this._procID == info._procID;
        }

        public bool belongToSameProcess(SystemWindow win)
        {
            return this._procID == win.Process.Id;
        }

        public void update(SystemWindow win)
        {
            this._procID = win.Process.Id;
            this._procName = win.Process.ProcessName;
            this._winTitle = win.Title;
            this._winHandle = win.HWnd;
        }
    }
}
