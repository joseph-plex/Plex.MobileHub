using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Plex.MobileHub.Client.Interface.Windows;
namespace Plex.MobileHub.Client.Interface
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WinFactory wf = new WinFactory();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(wf.GetDatabaseSelectionWin32());
        }
    }
}
