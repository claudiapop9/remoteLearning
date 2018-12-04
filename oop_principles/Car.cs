

namespace OopPrinciples
{
    class Car : Vehicle
    {
        private int doors;

        
        public Car(Engine engine, string color, int doors):base(engine,color,4)
        {
            this.doors = doors;
        }

        public override double ComputePrice()
        {
            double price = 2000;
            price += Engine.PriceProperty;
            price += doors * 1000;
            if (Color == "white" | Color == "black" | Color == "gray") {
                price += 200;
            } else if (Color == "red" | Color == "blue" | Color == "yellow"){
                price += 600;
            } else if (Color == "burgundy") {
                price += 800;
            }

            return price;

        }

        public override string ToString()
        {
            string str= "Your awesome car has:\n\n";
            str += Engine.ToString()+"\n";
            str += "Color: " + Color;
            str += "\nDoors: " + doors.ToString();
            str += "\nWheels:" + Wheels.ToString() + "\n";

            return str;
        }
    }
}
