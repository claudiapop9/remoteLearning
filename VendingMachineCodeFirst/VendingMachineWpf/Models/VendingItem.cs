using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineWpf.Models
{
    public class VendingItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }


        public VendingItem(int id, string name, int quantity, double price)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }
}
