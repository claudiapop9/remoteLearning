
namespace VendingMachine
{
    class Product
    {
        private int id;
        private string name;
        private int quantity;
        private double price;


        public Product(int id,string name, int quantity, double price)
        {
            this.Id = id;
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

        public int Id { get => id; set => id = value; }

        public override string ToString()
        {
            return "Produs:" + this.id + " " + this.name + " quantity: " + quantity + " price: " + price;

        }
    }
}
