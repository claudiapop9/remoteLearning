using System;

[assembly:log4net.Config.XmlConfigurator(Watch = true)]

namespace VendingMachineAdmin
{
    class Program
    {
        public static void Main(String[] args)
        {
            UI ui = new UI();
            ui.Run();
        }
    }
}