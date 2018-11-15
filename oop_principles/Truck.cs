using System;
using System.Collections.Generic;
using System.Text;

namespace oop_principles
{
    class Truck : Vehicle
    {
        private int doors;

        public Truck(){ this.doors = 2; }

        public Truck(Engine engine, string color, int wheels)
        {
            base.Engine = engine;
            base.Color = color;
            base.Wheels = wheels;
            this.doors = 2;

            
        }

        public override double computePrice()
        {
            double price = 5000;
            price += Engine.priceProperty;
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
            str += "\nDoors: " + this.doors.ToString();

            return str;
        }
        
    }
}
