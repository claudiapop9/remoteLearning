using System;
using System.Collections.Generic;

namespace VendingMachineAdmin
{
    class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
       
        
        public Product(string name, int quantity, double price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public Product(int id,string name, int quantity, double price)
        {
            ProductId = id;
            Name = name;
            Quantity = quantity;
            Price = price;
        }



        public override String ToString()
        {

            // return $"Id: {ProductId} Name: {Name} Quantity: {Quantity} Price: {Price}";
            return $"Name: {Name} Quantity: {Quantity} Price: {Price}";

        }
    }
}

