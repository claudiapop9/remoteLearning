using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace VendingMachineAdmin
{
    class AdminSocket

    {
        private static string serverIp = "localhost";
        private static int port = 8080;
        private Socket sender;

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AdminSocket()
        {
            this.sender = StartClient();
        }

        public static Socket StartClient()
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint);

                    byte[] bytes = new byte[1024];
                    byte[] msg = Encoding.ASCII.GetBytes("Success");

                    sender.Send(msg);

                    return sender;
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("Could not connect to server :(");
                    log.Error("ArgumentNullException : {0}", ane);
                }
                catch (SocketException se)
                {
                    Console.WriteLine("Could not connect to server :(");
                    log.Error("SocketException : {0}", se);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not connect to server :(");
                    log.Error("Unexpected exception : {0}", e);
                }
            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine("Could not connect to server :(");
            }

            return null;
        }

        public string Get(string message)
        {
            try
            {
                SendMessage(message);
                var response = ReceiveMessage();
                return response;
            }
            catch (Exception e)
            {
                log.Error(e);
            }

            return null;
        }

        public void SendMessage(string message)
        {
            try
            {
                byte[] msg = Encoding.ASCII.GetBytes(message);
                sender.Send(msg);
            }
            catch (Exception e)
            {
                log.Error("SEND Message" + e);
            }
        }

        public string ReceiveMessage()
        {
            try
            {
                byte[] bytes = new byte[1024];
                int bytesRec = sender.Receive(bytes);
                string messageReceived = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                return messageReceived;
            }
            catch (Exception e)
            {
                log.Error("Receive Message" + e);
            }

            return null;
        }

        public void ReleaseSocket()
        {
            try
            {
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}