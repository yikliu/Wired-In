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
/// The Analyzer namespace.
/// </summary>
namespace WiredIn.Analyzer
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    using ManagedWinapi.Hooks;
    using ManagedWinapi.Windows;
    
    using WiredIn.DataStructure;
    using WiredIn.Globals;
    using WiredIn.Log;
    using WiredIn.UserActivity;
    using WiredIn.Visualization.Visualizer;

    /// <summary>
    /// A worker is responsible for monitoring the activity queue and determining the transition direction
    /// and speed of the visualization
    /// </summary>
    public class Worker : IRunner
    {
        #region Fields

        /// <summary>
        /// The EVEN t_ OBJEC t_ NAMECHANGE
        /// </summary>
        private const uint EVENT_OBJECT_NAMECHANGE = 0x800C;

        /// <summary>
        /// The WINEVEN t_ OUTOFCONTEXT
        /// </summary>
        private const uint WINEVENT_OUTOFCONTEXT = 0;
        /// <summary>
        /// The Key Down
        /// </summary>
        private const uint WM_KEYDOWN = 0x0100;
        /// <summary>
        /// The Left Mouse Up
        /// </summary>
        private const uint WM_LBUTTONUP = 0x0202;
        /// <summary>
        /// The Middle Mouse Up
        /// </summary>
        private const uint WM_MBUTTONUP = 0x0208;
        /// <summary>
        /// The Horizontal Mouse Wheel
        /// </summary>
        private const uint WM_MOUSEHWHEEL = 0x020E;

        //Global mouse keyboard hook
        /// <summary>
        /// The w m_ mousewheel
        /// </summary>
        private const uint WM_MOUSEWHEEL = 0x020A;
        /// <summary>
        /// The w m_ rbuttonup
        /// </summary>
        private const uint WM_RBUTTONUP = 0x0205;

        /// <summary>
        /// The configuration
        /// </summary>
        private ConfigVariables config = ConfigVariables.GetConfigVariables();
        /// <summary>
        /// The global timer
        /// </summary>
        private GlobalTimer globalTimer = GlobalTimer.GetGlobalTimer();
        /// <summary>
        /// The is_running
        /// </summary>
        private bool is_running = false;
        /// <summary>
        /// The judge
        /// </summary>
        private Judge judge; //Judge attentive curState
        /// <summary>
        /// The last action was wheel
        /// </summary>
        bool LastActionWasWheel = false;
        /// <summary>
        /// The ll key hook
        /// </summary>
        private LowLevelKeyboardHook llKeyHook = new LowLevelKeyboardHook();
        /// <summary>
        /// The ll mouse hook
        /// </summary>
        private LowLevelMouseHook llMouseHook = new LowLevelMouseHook();
        /// <summary>
        /// The logger
        /// </summary>
        private Logger logger; //Logger
        /// <summary>
        /// The old title
        /// </summary>
        private string oldTitle, newTitle;
        /// <summary>
        /// The activity queue
        /// </summary>
        private Queue<Activity> theActivityQueue;
        /// <summary>
        /// The visualizer
        /// </summary>
        private AbstractVisualizer visualizer;

        /// <summary>
        /// The window
        /// </summary>
        private WinEventDelegate winEvtDelegate = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Worker constructor
        /// </summary>
        /// <param name="v">The v.</param>
        public Worker(AbstractVisualizer v)
        {
            theActivityQueue = new Queue<Activity>();
            judge = new Judge();
            logger = new Logger(theActivityQueue);
            this.visualizer = v;
        }

        #endregion Constructors

        #region Delegates

        /// <summary>
        /// Delegate WinEventDelegate
        /// </summary>
        /// <param name="hWinEventHook">The h win event hook.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="idObject">The identifier object.</param>
        /// <param name="idChild">The identifier child.</param>
        /// <param name="dwEventThread">The dw event thread.</param>
        /// <param name="dwmsEventTime">The DWMS event time.</param>
        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        #endregion Delegates

        #region Methods

        /// <summary>
        /// Catches the key press activity.
        /// </summary>
        /// <param name="key">The key.</param>
        public void CatchKeyPressActivity(KeyPress key)
        {
            key.Catched = true;
            key.StateString = judge.CurState.ToString();
            System.Console.WriteLine("Key Hit");
            judge.ResetDormantTimer();
        }

        /// <summary>
        /// Catches the mouse click activity.
        /// </summary>
        /// <param name="mouse">The mouse.</param>
        public void CatchMouseClickActivity(MouseClick mouse)
        {
            mouse.Catched = true;
            mouse.StateString = judge.CurState.ToString();
            System.Console.WriteLine("Mouse Click");
            judge.ResetDormantTimer();
        }

        /// <summary>
        /// Catches the shut down activity.
        /// </summary>
        /// <param name="sd">The sd.</param>
        public void CatchShutDownActivity(ShutDown sd)
        {
            sd.Catched = true;
            sd.StateString = judge.CurState.ToString();
        }

        /// <summary>
        /// Catches the start up activity.
        /// </summary>
        /// <param name="su">The su.</param>
        public void CatchStartUpActivity(StartUp su)
        {
            su.Catched = true;
            su.StateString = judge.CurState.ToString();
        }

        /// <summary>
        /// Catches the window change activity.
        /// </summary>
        /// <param name="ac">The ac.</param>
        public void CatchWindowChangeActivity(WindowChangeActivity ac)
        {
            judge.CheckOnTask(ac.NewWindow);
            ac.Catched = true;
            ac.StateString = judge.CurState.ToString();
        }

        /// <summary>
        /// Enqueues the activity.
        /// </summary>
        /// <param name="a">A.</param>
        public void EnqueueActivity(Activity a)
        {
            if (!is_running)
            {
                return;
            }

            a.Accept(this); //deal with this activity first

            //enqueue for logging
            try
            {
                lock (this)
                {
                    theActivityQueue.Enqueue(a);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Determines whether this instance is running.
        /// </summary>
        /// <returns><c>true</c> if this instance is running; otherwise, <c>false</c>.</returns>
        public bool IsRunning()
        {
            return is_running;
        }

        /// <summary>
        /// prompt the ESM windows if procrastination is found
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public void Procrastinate(object sender, ProcrastinateEvengArgs e)
        {
            this.Stop();
            WiredIn.UI.ESMForm esm= new WiredIn.UI.ESMForm(e.span);
            bool ESMSuccess = (esm.ShowDialog() == DialogResult.OK);
            if (ESMSuccess)
            {
                this.Start();
            }
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        public void SetUp()
        {
            SetUpEventListeners();
            this.globalTimer.SetUp();
            this.judge.SetUp();
            this.judge.OnJudgeStateChange += this.StateChanged;
            this.judge.OnProcrastinate += this.Procrastinate;

            this.visualizer.SetUp();
            this.logger.SetUp();

            is_running = false;
        }

        /// <summary>
        /// Attaches the config listeners.
        /// </summary>
        public void SetUpEventListeners()
        {
            SetUpMouseKeyHooks();
            SetUpWindowChangeHook();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            this.globalTimer.Start();
            this.judge.Start();
            this.visualizer.Start();
            this.logger.Start();
            llMouseHook.StartHook();
            llKeyHook.StartHook();
            is_running = true;
            SystemWindow win = ManagedWinapi.Windows.SystemWindow.ForegroundWindow;
            judge.CheckOnTask(new WindowInfo(win));
        }

        /// <summary>
        /// States the changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="JudgeStateEventArgs"/> instance containing the event data.</param>
        public void StateChanged(object sender, JudgeStateEventArgs e)
        {
            visualizer.ChangeState(e.newState);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            llMouseHook.Unhook();
            llKeyHook.Unhook();
            this.judge.Stop();
            this.visualizer.Stop();
            this.logger.Stop();
            this.globalTimer.Stop();
            is_running = false;
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        public void TearDown()
        {
            this.judge.TearDown();
            this.visualizer.TearDown();
            this.logger.TearDown();
            this.globalTimer.TearDown();
            is_running = false;
        }

        /// <summary>
        /// Wins the event proc.
        /// </summary>
        /// <param name="hWinEventHook">The h win event hook.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="idObject">The identifier object.</param>
        /// <param name="idChild">The identifier child.</param>
        /// <param name="dwEventThread">The dw event thread.</param>
        /// <param name="dwmsEventTime">The DWMS event time.</param>
        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (eventType == EVENT_OBJECT_NAMECHANGE)
            {
                newTitle = GetActiveWindowTitle();
                if ((null != newTitle) && !newTitle.Equals(oldTitle))
                {
                    oldTitle = newTitle;
                    SystemWindow window = SystemWindow.ForegroundWindow;
                    this.EnqueueActivity(new WindowChangeActivity(new WindowInfo(window), DateTime.Now, visualizer.GetScore()));
                }
            }
        }

        /// <summary>
        /// Gets the foreground window.
        /// </summary>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Gets the window text.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="text">The text.</param>
        /// <param name="count">The count.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        /// <summary>
        /// Sets the win event hook.
        /// </summary>
        /// <param name="eventMin">The event minimum.</param>
        /// <param name="eventMax">The event maximum.</param>
        /// <param name="hmodWinEventProc">The hmod win event proc.</param>
        /// <param name="lpfnWinEventProc">The LPFN win event proc.</param>
        /// <param name="idProcess">The identifier process.</param>
        /// <param name="idThread">The identifier thread.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        /// <summary>
        /// Gets the active window title.
        /// </summary>
        /// <returns>System.String.</returns>
        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            IntPtr handle = IntPtr.Zero;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        /// <summary>
        /// Lls the hook_ message intercepted.
        /// </summary>
        /// <param name="evt">The evt.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        void llHook_MessageIntercepted(LowLevelMessage evt, ref bool handled)
        {
            uint msg = (uint)evt.Message;
            switch (msg)
            {
                case WM_MOUSEWHEEL:
                case WM_MOUSEHWHEEL:
                    if (!LastActionWasWheel)
                    {
                        System.Console.WriteLine("Mouse Wheel");
                        LastActionWasWheel = true;
                    }
                    break;
                case WM_LBUTTONUP:
                case WM_MBUTTONUP:
                case WM_RBUTTONUP:
                    System.Console.WriteLine("Mouse Click");
                    this.EnqueueActivity(new MouseClick(DateTime.Now, visualizer.GetScore()));
                    LastActionWasWheel = false;
                    break;
                case WM_KEYDOWN:
                    this.EnqueueActivity(new KeyPress(DateTime.Now, visualizer.GetScore()));
                    LastActionWasWheel = false;
                    break;
            }
        }

        /// <summary>
        /// Sets up mouse key hooks.
        /// </summary>
        private void SetUpMouseKeyHooks()
        {
            llMouseHook.MessageIntercepted += llHook_MessageIntercepted;
            llKeyHook.MessageIntercepted += llHook_MessageIntercepted;
        }

        /// <summary>
        /// Sets up window change hook.
        /// </summary>
        private void SetUpWindowChangeHook()
        {
            winEvtDelegate = new WinEventDelegate(WinEventProc);
            IntPtr titleChangeHook = SetWinEventHook(EVENT_OBJECT_NAMECHANGE,
                                                                                            EVENT_OBJECT_NAMECHANGE,
                                                                                            IntPtr.Zero,
                                                                                            winEvtDelegate,
                                                                                            0,
                                                                                            0,
                                                                                            WINEVENT_OUTOFCONTEXT);
        }

        #endregion Methods
    }
}