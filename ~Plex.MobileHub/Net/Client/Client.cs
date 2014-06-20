using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Plex.PMH.Net.Client
{
    public class Client
    {
            /// <summary>
            /// Formats a Message Object into an xml document and sends it to a specified TCP/IP
            /// This function only supports IPv4 Addresses
            /// </summary>
            /// <param name="IPv4"> The IP to send the XML Document</param>
            /// <param name="Port"> The Port to send the XML Document</param>
            /// <param name="Msg"> The Message Object to be converted to an XML Document</param>
            public static void SendSignal(String IPv4, int Port, Message Msg)
            {
                    IPAddress IP = Dns.GetHostAddresses(IPv4).Where(address => address.AddressFamily == AddressFamily.InterNetwork).First();
                    SendSignal(IP, Port, Msg);
            }
            /// <summary>
            /// Formats a Message Object into an xml document and sends it to a specified TCP/IP
            /// This function only supports IPv4 Addresses
            /// </summary>
            /// <param name="IPv4"> The IP to send the XML Document </param>
            /// <param name="Port"> The Port to send the XML Document </param>
            /// <param name="Msg">  The Message Object to be converted to an XML Document </param>
            public static void SendSignal(IPAddress IPv4, int Port, Message Msg)
            {
                    IPEndPoint remoteEP = new IPEndPoint(IPv4, Port);
                    SendSignal(remoteEP, Msg);
            }

            /// <summary>
            /// Formats a Message Object into an xml document and sends it to a specified TCP/IP
            /// This function only supports IPv4 Addresses
            /// </summary>
            /// <param name="RemoteEndPoint"> The remote Endput to send the Xml Document</param>
            /// <param name="Msg"> The Message Object to be converted to an XML Document </param>
            public static void SendSignal(IPEndPoint RemoteEndPoint, Message Msg)
            {
                    Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    s.BeginConnect(RemoteEndPoint, new AsyncCallback(OnBeginConnect), new SendData(s, Msg));
                
            }

            /// <summary>
            /// Asynchronous Method Call which sends the message.
            /// </summary>
            /// <param name="ar">For more information see : http://msdn.microsoft.com/en-us/library/system.iasyncresult.aspx</param>
            private static void OnBeginConnect(IAsyncResult ar)
            {
                    var data = (SendData)ar.AsyncState;
                    var msg = data.msg.GetBytes();
                    data.socket.EndConnect(ar);
                    data.socket.BeginSend(msg, 0, msg.Length, SocketFlags.None,
                        (IAsyncResult a) => { ((Socket)a.AsyncState).EndSend(a); ((Socket)a.AsyncState).Dispose(); }, data.socket);
            }

            /// <summary>
            /// Represents the union of Message and destination.
            /// </summary>
            internal class SendData
            {
                public Message msg;
                public Socket socket;

                public SendData(Socket socket, Message msg)
                {
                    this.socket = socket;
                    this.msg = msg;
                }
            }
    }
}
