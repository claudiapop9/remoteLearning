using System;
using System.Collections.Generic;
using System.Text;

namespace oop_principles
{
    class Controller
    {
        private Car car;
        private Motorcycle motorcycle;
        private Truck truck;

        public Controller(){}
        public Controller(Car car)
        {
            this.car = car;
        }
        public Controller(Motorcycle motorcycle)
        {
            this.motorcycle = motorcycle;
        }
        public Controller(Truck truck)
        {
            this.truck = truck; ;
        }
        public Car CarProperty { get; set; }
        public Motorcycle MotorcycleProperty { get; set; }
        public Truck TruckProperty { get; set; }

    }
}
