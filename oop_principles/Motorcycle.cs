using System;
using System.Collections.Generic;
using System.Text;

namespace oop_principles
{
    class Motorcycle : Vehicle

    {
        public Motorcycle(){ base.Wheels = 2; }

        public Motorcycle(Engine engine, string color)
        {
            base.Engine = engine;
            base.Color = color;
            base.Wheels = 2;
        }

        public override double computePrice()
        {
            double price = 1000;
            price += Engine.priceProperty;
            if (Color == "white" | Color == "black" | Color == "gray")
            {
                price += 200;
            }
            else if (Color == "red")
            {
                price += 600;
            }
            return price;
        }

        public override string ToString()
        {
            string str = "Your safe motorcycle has:\n\n";
            str += Engine.ToString() + "\n";
            str += "Color: " + Color + "\n";
            str += "Wheels:" + Wheels.ToString() + "\n";
            return str;
        }
    }
}
