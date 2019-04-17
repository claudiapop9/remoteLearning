using System;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace VendingMachineAdmin
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static int Main(String[] args)
        {

            AdminSocket.StartListening("Admin sent this as result");
            log.Info("Admin Log");
            return 0;
        }
    }
}
