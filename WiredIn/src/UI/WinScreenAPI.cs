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
/// The WiredIn namespace.
/// </summary>
namespace WiredIn
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Class WinScreenAPI.
    /// </summary>
    class WinScreenAPI
    {
        #region Fields

        /// <summary>
        /// The s m_ cxscreen
        /// </summary>
        private const int SM_CXSCREEN = 0;
        /// <summary>
        /// The s m_ cyscreen
        /// </summary>
        private const int SM_CYSCREEN = 1;
        /// <summary>
        /// The sw p_ showwindow
        /// </summary>
        private const int SWP_SHOWWINDOW = 64; // 0×0040

        /// <summary>
        /// The HWN d_ top
        /// </summary>
        private static IntPtr HWND_TOP = IntPtr.Zero;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the screen x.
        /// </summary>
        /// <value>The screen x.</value>
        public static int ScreenX
        {
            get
            {
                return GetSystemMetrics(SM_CXSCREEN);
            }
        }

        /// <summary>
        /// Gets the screen y.
        /// </summary>
        /// <value>The screen y.</value>
        public static int ScreenY
        {
            get { return GetSystemMetrics(SM_CYSCREEN);}
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the system metrics.
        /// </summary>
        /// <param name="which">The which.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int which);

        /// <summary>
        /// Sets the window position.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="hwndInsertAfter">The HWND insert after.</param>
        /// <param name="X">The x.</param>
        /// <param name="Y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="flags">The flags.</param>
        [DllImport("user32.dll")]
        public static extern void SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
            int X, int Y, int width, int height, uint flags);

        /// <summary>
        /// Sets the win full screen.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        public static void SetWinFullScreen(IntPtr hwnd)
        {
            SetWindowPos(hwnd, HWND_TOP, 0, 0, ScreenX, ScreenY, SWP_SHOWWINDOW);
        }

        #endregion Methods
    }
}