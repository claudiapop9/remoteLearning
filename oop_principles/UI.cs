using System;
//@Paul  unused....
using System.Collections.Generic;
using System.Text;

namespace oop_principles
{
    class UI
    {
        private Controller controller = new Controller();

        public UI(){}

        public void MainMenu() {
            string str = "\t-------------Welcome!------------\n";
            str += "Choose your vehicle:\n";
            str += "1.Car\n";
            str += "2.Motorcycle\n";
            str += "3.Truck\n";
            str += "0.Exit";
            Console.WriteLine(str);
        }
        public Engine EngineMenu() {

            Engine engine = new Engine();

            Console.WriteLine("Introduce engine size cm:");
            int dim=Int32.Parse(Console.ReadLine());
            engine.dimProperty=dim;

            Console.WriteLine("Fuel type:");
            engine.fuelProperty = Console.ReadLine();         

            return engine;
        }
        
        public void CarMenu() {

            
            Console.WriteLine("\t----------------CAR----------------\n");
            Console.WriteLine("No of doors(2|4)");
            int doors = Int32.Parse(Console.ReadLine());
            Vehicle car = new Car(doors);

            car.Engine = EngineMenu();
            Console.WriteLine("Color: white,black,gray,red,blue,yellow,burgundy");
            car.Color= Console.ReadLine();            
            this.controller.VehicleProperty = car;
            Console.WriteLine("\n"+car.ToString());
            Console.WriteLine("Price:" +car.computePrice());
            Console.ReadKey();

        }
        public void MotorcycleMenu() {

            Vehicle motorcycle = new Motorcycle();

            Console.WriteLine("\t----------------MOTORCYCLE----------------\n");
            motorcycle.Engine = EngineMenu();
            Console.WriteLine("Color: white,black,gray,red");
            motorcycle.Color = Console.ReadLine();

            this.controller.VehicleProperty = motorcycle;
            Console.WriteLine("\n"+motorcycle.ToString());
            Console.WriteLine("Price:" + motorcycle.computePrice());
            Console.ReadKey();
        }

        public void TruckMenu() {

            Vehicle truck = new Truck();

            Console.WriteLine("\t----------------TRUCK----------------\n");
            truck.Engine = EngineMenu();
            Console.WriteLine("Color: white,black,gray,red");
            truck.Color = Console.ReadLine();
            Console.WriteLine("No of wheels 4|6|8");
            truck.Wheels= Int32.Parse(Console.ReadLine());

            this.controller.VehicleProperty = truck;
            Console.WriteLine("\n"+truck.ToString());
            Console.WriteLine("Price:" + truck.computePrice());
            Console.ReadKey();

        }

        public void run() {
            MainMenu();
            string option = Console.ReadLine();
            switch (option) {
                case "1":
                    CarMenu();
                    break;
                case "2":
                    MotorcycleMenu();
                    break;
                case "3":
                    TruckMenu();
                    break;
                case "4":
                    break;
                default:
                    Console.WriteLine("There is not this option");
                    break;
            }

        }

    }
}
