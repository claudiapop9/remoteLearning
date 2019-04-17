using System;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace VendingMachineAdmin
{
    class Program
    {

        public static int Main(String[] args)
        {
            AdminSocket.StartListening("Admin sent this as result");
            return 0;
        }
    }
}
