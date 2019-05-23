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

        private void Purchase_Clicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
        }
    }
}
