using System;
using System.Collections.Generic;
using System.Text;

namespace OopPrinciples
{
    class Motorcycle : Vehicle

    {
       
        public Motorcycle(Engine engine, string color):base(engine,color,2)
        {
            
        }

        public override double computePrice()
        {
            double price = 1000;
            price += Engine.PriceProperty;
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
