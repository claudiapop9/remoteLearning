using System;
using System.Collections.Generic;
using System.Text;

namespace oop_principles
{
    class Car : Vehicle
    {
        private int doors;

        public Car(){ base.Wheels = 4;}

        public Car(int doors)
        {
            this.doors = doors;
            base.Wheels = 4;
        }

        public Car(Engine engine, string color, int doors)
        {
            base.Engine = engine;
            base.Color = color;
            base.Wheels = 4;
            this.doors = doors;

        }

        public override double computePrice()
        {
            double price = 2000;
            price += Engine.priceProperty;
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

        public int DoorProperty
        {
            get { return this.doors; }
            set { this.doors = value; }
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
