using System.Windows;
using System.Windows.Controls;
using VendingMachineWpf.ViewModels;


namespace VendingMachineWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MachineViewModel machine = new MachineViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = machine;
        }

        private void PurchaseCash_Clicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            machine.PurchaseCash(button.DataContext);
        }

        private void Insert1_Clicked(object sender, RoutedEventArgs e)
        {
            machine.InsertChange(1);
        }

        private void Insert5_Clicked(object sender, RoutedEventArgs e)
        {
            machine.InsertChange(5);
        }

        private void Insert10_Clicked(object sender, RoutedEventArgs e)
        {
            machine.InsertChange(10);
        }
    }
}
