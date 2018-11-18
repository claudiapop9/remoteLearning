using System;
using System.Collections.Generic;
using System.Text;

namespace oop_principles
{
    class Controller
    {
        
        private Vehicle vehicle;

        public Controller(){}
        public Controller(Vehicle vehicle)
        {
            this.vehicle = vehicle;
        }
        
        public Vehicle VehicleProperty { get; set; }
        

    }
}
