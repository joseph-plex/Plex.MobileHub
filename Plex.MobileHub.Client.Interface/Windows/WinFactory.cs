using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Plex.MobileHub.Client.Interface.Views;

namespace Plex.MobileHub.Client.Interface.Windows
{
    public class WinFactory
    {
        public Form GetLoginSettingWin32()
        {
            LoginView view = new LoginView();
            DialogWin win32 = new DialogWin();

            win32.Controls.Add(view);
            
            win32.AcceptButton = view.AcceptButton;
            win32.CancelButton = view.CancelButton;

            view.AcceptButton.Click += (s, e) => win32.Close();
            view.CancelButton.Click += (s, e) => win32.Close();
            
            return win32;
        }

        public Form GetDatabaseManWin32()
        {
            DatabaseView dbv = new DatabaseView();
            DialogWin win32 = new DialogWin();

            win32.Controls.Add(dbv);

            return win32;
        }


        public Form GetLogsWin32()
        {
            LogsView view = new LogsView();
            DialogWin win32 = new DialogWin();
            win32.Controls.Add(view);
            return win32;
        }

        public Form CreateDatabaseAdditionWindow()
        {
            DialogWin win32 = new DialogWin();
            AddDatabase ad = new AddDatabase();

            ad.AddButton.Click += (s, e) => win32.Close();

            win32.AcceptButton = ad.AddButton;
            win32.CancelButton = ad.CancelButton;
            
            win32.Controls.Add(ad);
            return win32;
        }
    }
}
