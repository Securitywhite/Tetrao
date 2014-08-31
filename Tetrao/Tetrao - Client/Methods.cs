/*  
 *  Author: Avinash
 *  Date: August 30th, 2014
 *  Desc: Class to manage functions used for bot
 *  Version: 1.0
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;

namespace Tetrao___Client
{
    static class Methods
    {
        #region Function Pointers/Declaration
        public static long rambo = 13L;
        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int GetAsyncKeyState(long vkey);
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);
        [DllImport("User32.Dll", CharSet = CharSet.Unicode)]
        private static extern bool PostMessageA(IntPtr hwnd, uint msg, int wparam, IntPtr lparam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        [DllImport("user32.dll")]
        private static extern byte VkKeyScan(char chr);
        [DllImport("user32.dll")]
        static extern bool SetKeyboardState(byte[] lpKeyState);

        [DllImport("User32", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr SetParent(IntPtr oldchildhandle, IntPtr newhandle);

        [DllImport("wininet.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetOption(int hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        #endregion
        public static unsafe void SuppressWininetBehavior()
        {
            int option = (int)3/* INTERNET_SUPPRESS_COOKIE_PERSIST*/;
            int* optionPtr = &option;

            bool success = InternetSetOption(0, 81/*INTERNET_OPTION_SUPPRESS_BEHAVIOR*/, new IntPtr(optionPtr), sizeof(int));
        }

        //const int WM_CHAR = 0x102;
        /* End of WinAPI Hook-Ins*/
        //-------------------------------------------------\\

        public static void Click(int x, int y, WebBrowser webBrowser)
        {
            IntPtr handl = webBrowser.Handle;
            StringBuilder stringBuilder = new StringBuilder(100);
            while (stringBuilder.ToString() != "Internet Explorer_Server") // Some reason the browser processes name is Internet Explorer_Server
            {
                //Loop till grab proper handle
                handl = GetWindow(handl, 5u);
                GetClassName(handl, stringBuilder, stringBuilder.Capacity);
            }
            IntPtr intptr_3 = (IntPtr)(y << 16 | x);
            SendMessage(handl, 513u, IntPtr.Zero, intptr_3);
            //SendMessage(handl, 514u, IntPtr.Zero, intptr_3);
        }
        public static void EnterMethod(IntPtr browserHandle)
        {
            IntPtr bHandle = browserHandle;
            StringBuilder stringBuilder = new StringBuilder(100);
            while (stringBuilder.ToString() != "Internet Explorer_Server")
            {
                bHandle = GetWindow(bHandle, 5u);
                GetClassName(bHandle, stringBuilder, stringBuilder.Capacity);
            }
            PostMessageA(bHandle, 257u, Convert.ToInt32(rambo), IntPtr.Zero);
            PostMessageA(bHandle, 256u, Convert.ToInt32(rambo), IntPtr.Zero);
            PostMessageA(bHandle, 257u, Convert.ToInt32(rambo), IntPtr.Zero);
        }
        public static void NewClickMethod(IntPtr browserHandle)
        {
        }
        public static void SpeechMethod(string message, WebBrowser Browser)
        {
            IntPtr bHandle = Browser.Handle;
            //get webbrowsers handle
            StringBuilder stringBuilder = new StringBuilder(100);
            while (stringBuilder.ToString() != "Internet Explorer_Server")
            {
                bHandle = GetWindow(bHandle, 5u);
                GetClassName(bHandle, stringBuilder, stringBuilder.Capacity);
            }
            checked
            {
                for (int i = 0; i < message.Length; i++)
                {
                    //while i is not equal the the string length that is gonna be spammed
                    char char_ = message[i];
                    //char_ is equal the the current value of i which is constantly being updated and so...
                    PostMessageA(bHandle, 0x102, char_, IntPtr.Zero);
                    //0x102 is the code for the Windows Message Char
                    //we then type whatever we want to with PostMessageA 
                }
            }
        }
        public static void ToggleShift(bool on, bool off) //Deprecated, found newer way. This method has a delay.
        {
            if (on == true && off == false)
            {
                byte[] array = new byte[256];
                array[1] = 0;
                array[16] = 128;
                array[160] = 128;
                SetKeyboardState(array);
            }
            if (on == false && off == true)
            {
                byte[] array = new byte[256];
                array[1] = 0;
                array[16] = 0;
                array[160] = 0;
                SetKeyboardState(array);
            }
        }

    }

}

