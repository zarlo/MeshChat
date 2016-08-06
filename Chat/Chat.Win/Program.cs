﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

namespace Chat.Win
{
    static class Program
    {

        
        /// <summary>
        /// The main entry point for the application.
        // </summary>
        [STAThread]
        static void Main()
        {
            
            Boot.PreInit();
            Boot.Init();
            Boot.PostInit();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());   
        }
    }
}
