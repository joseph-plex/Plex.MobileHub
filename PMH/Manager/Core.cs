using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Plex.PMH
{
    public delegate void Subscriber(Object sender, EventArgs e);

    public class Manager
    {
        private static Manager manager = new Manager();
        public static Manager Instance
        {
            get
            {
                return manager;
            }
        }

        private Manager()
        {
        }
    }
}