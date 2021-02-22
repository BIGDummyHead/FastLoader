using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FastandLow.Bootstrap
{
    /// <summary>
    /// <see cref="Console"/> Controller
    /// </summary>
    public static class ConsoleLoader
    {
        /// <summary>
        /// Info on the User's Console
        /// </summary>
        public struct Info
        {
            /// <summary>
            /// should the console open?
            /// </summary>
            public bool OpenConsole { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="win"></param>
        /// <param name="i"></param>
        /// <returns></returns>

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr win, int i);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();

        /// <summary>
        /// Show the console only if it is active!
        /// </summary>
        public static void ShowConsole()
        {
            if (!consoleOpen)
                return;

            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, showCon);
        }

        /// <summary>
        /// Open the Console!
        /// </summary>
        public static void OpenConsole()
        {
            if (consoleOpen)
                ShowConsole();
            else
            {
                AllocConsole();
                StreamWriter streamWriter = new StreamWriter(Console.OpenStandardOutput());
                streamWriter.AutoFlush = true;
                Console.SetError(streamWriter);
                Console.SetOut(streamWriter);

                consoleOpen = true;
            }
        }

        /// <summary>
        /// Close the console completely
        /// </summary>
        public static void CloseConsole()
        {
            if (!consoleOpen)
                return;

            IntPtr handle = GetConsoleWindow();

            ShowWindow(handle, hideCon);
        }

        private static bool consoleOpen = false;

        private static int showCon = 5;

        private static int hideCon = 0;
    }
}
