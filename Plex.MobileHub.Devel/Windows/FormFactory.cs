using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Plex.MobileHub.Devel.Views;
using Plex.MobileHub.Devel.Logic;

namespace Plex.MobileHub.Devel.Windows
{
    public class FormFactory
    {
        public Form CreateLoadingForm(Action action)
        {
            var loadingView = new ActionForm(new LoadingControl());
            loadingView.Load += (s, e) =>
            {
                new Thread(() =>
                    {
                        try
                        {
                            action();
                        }
                        catch (Exception x)
                        {
                            MessageBox.Show(x.ToString());
                        }
                        finally
                        {
                            loadingView.InvokeIfRequired(() => loadingView.Close());
                        }
                    }).Start();
            };
            return loadingView;
        }

        public Form CreateApplicationSelection()
        {
            var form = new ActionForm(new AppListSelect());
            return form;
        }
    }
}
