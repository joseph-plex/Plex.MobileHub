using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.PMH.Net
{
    public delegate void Subscriber(Object sender, EventArgs e);

    public class MessageRecievedEventArgs : EventArgs 
    {
        public Message msg;
    }
}
