using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace VendingMachineCodeFirst
{
    class SocketCommunication
    {
        private Socket handler;

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SocketCommunication()
        {
            this.handler = StartListening();
        }
        
        public static Socket StartListening()
        {
            byte[] bytes = new Byte[1024];

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");

                    Socket handler = listener.Accept();
                    string data = null;

                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("Success") > -1)
                        {
                            break;
                        }
                    }
                    
                    Console.WriteLine("Text received : {0}", data);
                    Console.ReadKey();
                    return handler;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return null;
        }

       
        public bool IsConnected()
        {
            return handler != null;
        }

        public void SendData(string data)
        {
            try
            {
                byte[] msg = Encoding.ASCII.GetBytes(data);

                handler.Send(msg);
            }
            catch (Exception e)
            {
                log.Error("SEND DATA " + e);
            }
        }

        public string ReceiveMessage()
        {
            string data = null;
            try
            {
                byte[] bytes = new Byte[1024];
                int bytesRec2 = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec2);

                Console.WriteLine("Text received 2 : {0}", data);
                Console.ReadKey();
                return data;
            }
            catch (Exception e)
            {
                log.Error("RECEIVE " + e);
            }

            return data;
        }

        public void ReleaseSocket()
        {
            try
            {
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception ex)
            {
                log.Error("RELEASE "+ ex);
            }

        }
    }
}