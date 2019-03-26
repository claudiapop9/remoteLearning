[assembly: log4net.Config.XmlConfigurator(Watch =true)]

namespace VendingMachineCodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            UI ui = new UI();
            ui.Run();
        }
    }
}
