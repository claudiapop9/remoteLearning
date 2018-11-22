using System;
using System.Collections.Generic;
using System.Text;

namespace oop_principles
{
    abstract class Vehicle
    {
        private Engine engine;
        private string color;
        private int no_wheels;

        //@Paul default...
        public Vehicle(){}

        public Vehicle(Engine engine, string color, int no_wheels)
        {
            this.engine = engine;
            this.color = color;
            this.no_wheels = no_wheels; 
        }

        public abstract double computePrice();

        public string Color { get => color; set => color = value; }
        public int Wheels { get => no_wheels; set => no_wheels = value; }
        internal Engine Engine { get => engine; set => engine = value; }
    }
}
