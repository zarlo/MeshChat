using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Drawing;

namespace Chat.Win
{
    static class Program
    {

        static NotifyIcon NotifyIcon;
        static ContextMenuStrip ContextMenu;

        /// <summary>
        /// The main entry point for the application.
        // </summary>
        [STAThread]
        static void Main()
        {
            Globals.isLogedin = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            NotifyIcon = new NotifyIcon();
            NotifyIcon.Text = "Mesh Chat";
            NotifyIcon.Icon= Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);
            NotifyIcon.Visible = true;

            ContextMenu = new ContextMenuStrip();
            ContextMenu.Items.Add(new ToolStripMenuItem("Exit"));
            ContextMenu.Items[0].Click += new EventHandler(Exit_Clicked);
            ContextMenu.Items.Add(new ToolStripMenuItem("Show"));
            ContextMenu.Items[1].Click += new EventHandler(Show_Clicked);
            NotifyIcon.ContextMenuStrip = ContextMenu;
            Boot boot = new Boot();
            boot.PreInit();
            boot.Init();
            boot.PostInit();

            Application.Run(new Forms.Login());   
        }

        static void Show_Clicked(object sender, EventArgs e)
        {
            if (Globals.isLogedin)
            {
                Globals.Chatform.Show();
            }
        }

        static void Exit_Clicked(object sender, EventArgs e)
        {
            NotifyIcon.Dispose();
            ContextMenu.Dispose();
            Application.Exit();
        }
    }
}
