using System;
using System.Collections.Generic;
using System.Text;

namespace OopPrinciples
{
    class Truck : Vehicle
    {
        private int doors;

        public Truck(Engine engine, string color, int wheels):base(engine,color,wheels)
        {
           this.doors = 2;
        }

        public override double computePrice()
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
            //@Paul ToString casting unnecessary .. check in all project pls
            string str = "Your truck has:\n\n";
            str += Engine.ToString() + "\n";
            str += "Color: " + Color;
            str += "\nWheels: " + Wheels.ToString();
            str += "\nDoors: " + this.doors.ToString();

            return str;
        }
        
    }
}
