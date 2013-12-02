/**
 * WiredIn - Visual Reminder of Suspended Tasks
 *
 * The MIT License (MIT)
 * Copyright (c) 2012 Yikun Liu, https://github.com/yikliu
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in the
 * Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
/// <summary>
/// The Globals namespace.
/// </summary>
namespace WiredIn.Globals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using WiredIn.DataStructure;

    /// <summary>
    /// Class Worksphere.
    /// </summary>
    public class Worksphere
    {
        #region Fields

        //make it singleton
        /// <summary>
        /// The instance
        /// </summary>
        private static Worksphere instance;

        /// <summary>
        /// The acceptable handles
        /// </summary>
        private HashSet<IntPtr> acceptableHandles;
        /// <summary>
        /// The acceptable keywords
        /// </summary>
        private HashSet<string> acceptableKeywords;

        public HashSet<string> AcceptableKeywords
        {
            get { return acceptableKeywords; }
            set { acceptableKeywords = value; }
        }
        /// <summary>
        /// The acceptable proc names
        /// </summary>
        private HashSet<string> acceptableProcNames;

        public HashSet<string> AcceptableProcNames
        {
            get { return acceptableProcNames; }
            set { acceptableProcNames = value; }
        }
        /// <summary>
        /// The acceptable windows infos
        /// </summary>
        private List<WindowInfo> acceptableWindowsInfos;
        /// <summary>
        /// The acceptable window titles
        /// </summary>
        private List<String> acceptableWindowTitles;

        public List<String> AcceptableWindowTitles
        {
            get { return acceptableWindowTitles; }
            set { acceptableWindowTitles = value; }
        }
        /// <summary>
        /// The active view
        /// </summary>
        private Visualizer activeView = Visualizer.Rose;
        /// <summary>
        /// The custom visualizer name
        /// </summary>
        private string customVisualizerName = "N/A";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="Worksphere"/> class from being created.
        /// </summary>
        private Worksphere()
        {
            this.acceptableWindowsInfos = new List<WindowInfo>();
            this.acceptableKeywords = new HashSet<string>();
            this.acceptableProcNames = new HashSet<string>();
            this.acceptableHandles = new HashSet<IntPtr>();
            this.acceptableWindowTitles = new List<string>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the active view.
        /// </summary>
        /// <value>The active view.</value>
        public WiredIn.Globals.Visualizer ActiveView
        {
            get { return activeView; }
            set { activeView = value; }
        }

        /// <summary>
        /// Gets or sets the name of the custom visualizer.
        /// </summary>
        /// <value>The name of the custom visualizer.</value>
        public string CustomVisualizerName
        {
            get { return customVisualizerName; }
            set { customVisualizerName = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the work sphere.
        /// </summary>
        /// <returns>Worksphere.</returns>
        public static Worksphere GetWorkSphere()
        {
            if (null == instance)
            {
                instance = new Worksphere();
            }
            return instance;
        }

        /// <summary>
        /// Adds the keyword.
        /// </summary>
        /// <param name="w">The w.</param>
        public void AddKeyword(string w)
        {
            this.acceptableKeywords.Add(w.ToLowerInvariant());
        }

        /// <summary>
        /// Appends the white window information.
        /// </summary>
        /// <param name="info">The information.</param>
        public void AppendWhiteWindowInfo(WindowInfo info)
        {
            this.acceptableWindowsInfos.Add(info);
            this.acceptableWindowTitles.Add(info.WinTitle);
            this.acceptableProcNames.Add(info.ProcName);
            this.acceptableHandles.Add(info.WindowHandle);
        }

        /// <summary>
        /// Clears all.
        /// </summary>
        public void ClearAll()
        {
            this.acceptableKeywords.Clear();
            this.acceptableProcNames.Clear();
            this.acceptableWindowsInfos.Clear();
            this.acceptableWindowTitles.Clear();
            this.acceptableHandles.Clear();
        }

        /// <summary>
        /// Determines whether the specified handle contains handle.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <returns><c>true</c> if the specified handle contains handle; otherwise, <c>false</c>.</returns>
        public bool ContainsHandle(IntPtr handle)
        {
            return this.acceptableHandles.Contains(handle);
        }

        /// <summary>
        /// Determines whether [contains proc name] [the specified proc].
        /// </summary>
        /// <param name="proc">The proc.</param>
        /// <returns><c>true</c> if [contains proc name] [the specified proc]; otherwise, <c>false</c>.</returns>
        public bool ContainsProcName(string proc)
        {
            return this.acceptableProcNames.Contains(proc.ToLower());
        }

        /// <summary>
        /// Sizes the of intersection with keywords.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>System.Int32.</returns>
        public int SizeOfIntersectionWithKeywords(string title)
        {
            String[] words = title.ToLowerInvariant().Split(' ');
            int size = 0;
            foreach (String s in words)
            {
                if (this.acceptableKeywords.Contains(s))
                {
                    size++;
                }
            }
            return size;
        }

        #endregion Methods
    }
}