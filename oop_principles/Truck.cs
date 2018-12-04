

namespace OopPrinciples
{
    class Truck : Vehicle
    {
        private int doors;

        public Truck(Engine engine, string color, int wheels):base(engine,color,wheels)
        {
           this.Doors = 2;
        }

        public int Doors { get => doors; set => doors = value; }

        public override double ComputePrice()
        {
            double price = 5000;
            price += Engine.PriceProperty;
            price += Wheels * 500;
            
            if (Color == "white" | Color == "black" | Color == "gray")
            {
                price += 200;
            }
            else if (Color == "red" | Color == "blue")
            {
                price += 600;
            }
    
            return price;
        }
        public override string ToString()
        {
            string str = "Your truck has:\n\n";
            str += Engine.ToString() + "\n";
            str += "Color: " + Color;
            str += "\nWheels: " + Wheels.ToString();
            str += "\nDoors: " + this.Doors.ToString();

            return str;
        }
        
    }
}
