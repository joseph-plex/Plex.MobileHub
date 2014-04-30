using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

using System.Xml;
using System.Threading;
namespace Plex.PMH.Net.Server
{
    public class Server
    {

        private const int MaxPort = 65535;
        private const int MinPort = 49152;

        private IPAddress IP;
        private int port;

        public string IPv4
        {
            get
            {
                return IP.ToString();
            }
        }

        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }

        public bool IsListening
        {
            get;
            private set;
        }

        public event Subscriber OnStart;
        public event Subscriber OnStop;
        public event Subscriber OnRequest;

        private ManualResetEvent allDone = new ManualResetEvent(false);
        private Socket ear;

        public Server()
        {
            Random r = new Random();
            port = r.Next(MaxPort - MinPort) + MinPort;
            IP = Dns.GetHostAddresses(Dns.GetHostName()).Where(address => address.AddressFamily == AddressFamily.InterNetwork).First();
        }

        public void Start()
        {
            new Thread(() => StartListening()).Start();
        }

        private void StartListening()
        {
            ear = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ear.Bind(new IPEndPoint(IP, Port));
            ear.Listen(100);

            IsListening = true;
            if (OnStart != null) OnStart(this, EventArgs.Empty);

            for (allDone.Reset(); IsListening; allDone.WaitOne(), allDone.Reset())
                ear.BeginAccept(new AsyncCallback(AcceptCallback), ear);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();

            if (IsListening == false) return;

            StateObject state = new StateObject();
            Socket listener = (Socket)ar.AsyncState;

            state.workSocket = listener.EndAccept(ar);
            state.workSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        private void ReadCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            int bytesRead = state.workSocket.EndReceive(ar);

            try
            {
                if (OnRequest != null) OnRequest(this, new MessageRecievedEventArgs() 
                {
                    msg = new Message(state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead)).ToString()) 
                });
            }
            catch (XmlException)
            {
                state.workSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
            }
        }

        public void Stop()
        {
            IsListening = false;
            ear.Close();

            if (OnStop != null)
                OnStop(this, EventArgs.Empty);
        }

        internal class StateObject
        {
            public Socket workSocket = null;
            public const int BufferSize = 1024;
            public byte[] buffer = new byte[BufferSize];
            public StringBuilder sb = new StringBuilder();
        }
    }

}
