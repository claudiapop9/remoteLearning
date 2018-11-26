using System;
//@Paul unused directives
using System.Collections.Generic;
using System.Text;

namespace OopPrinciples
{
    class Engine
    {
        private int dimension;
        private string fuel;
        private double price;

        public Engine(int dimension,string fuel)
        {
            this.dimension = dimension;
            this.fuel = fuel;
            this.price = Cost();          
        }

        
        public double Cost() {
            try
            {
                double price = dimension * 1.05;
                if (this.fuel == "gas")
                {
                    price += 500;
                }
                else
                {
                    price += 300;
                }
                return price;
            }
            catch (Exception) { }

            return 2500;
        }

                        
        public double PriceProperty {
            get { return this.price; }
            set { this.price = value; }
        }

        public override string ToString()
        {
            string str = "Engine properties:\n";
            str += "engine size: " + this.dimension.ToString()+" cm^3\n";
            str += "fuel: " + this.fuel.ToString();
            return str;
        }
    }
}
