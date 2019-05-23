using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BusinessLogic;
using Newtonsoft.Json;

namespace VendingMachineWpf.ViewModels
{
    public class MachineViewModel: ObservableObject
    {
        public ObservableCollection<ProductViewModel> Items { get; private set; }
        public Controller ctrl = new Controller();

        public MachineViewModel()
        {
            Items = GetObservableCollection();
        }

        private ObservableCollection<ProductViewModel> GetObservableCollection()
        {
            ObservableCollection<ProductViewModel> items = new ObservableCollection<ProductViewModel>();
            try
            {
                List<ProductViewModel> productsModels = JsonConvert.DeserializeObject<List<ProductViewModel>>(ctrl.GetProducts());
                foreach (var product in productsModels)
                {
                    items.Add(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return items;
        }
    }
}
