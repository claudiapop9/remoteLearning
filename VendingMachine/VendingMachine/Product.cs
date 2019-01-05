

namespace VendingMachine
{
    class Product
    {

        private string name;
        private int quantity;
        private double price;

        public Product(string name, int quantity, double price)
        {
            this.name = name;
            this.quantity = quantity;
            this.price = price;
        }

        public void DecreaseQuantity()
        {
            this.quantity -= 1;
        }

        public string NameProperty
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int QuantityProperty
        {
            get { return this.quantity; }
            set { this.quantity = value; }
        }
        public double PriceProperty
        {
            get { return this.price; }
            set { this.price = value; }
        }
        public override string ToString()
        {
            return "Produs:" + this.name + " quantity: " + quantity + " price: " + price;

        }
    }
}
