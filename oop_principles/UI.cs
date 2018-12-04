using System;
//@Paul  unused....in all project
using System.Collections.Generic;
using System.Text;

namespace OopPrinciples
{
    class UI
    {
        //@Paul what's its purpose?  
        private Controller controller;

        //@Paul doesn't have to be explicit
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

            Console.WriteLine("Introduce engine size cm:");
            int dim=Int32.Parse(Console.ReadLine());
            Console.WriteLine("Fuel type:");
            string fuel = Console.ReadLine();         
            return new Engine(dim,fuel);
        }
        
        public void CarMenu() {

            
            Console.WriteLine("\t----------------CAR----------------\n");
            Console.WriteLine("No of doors(2|4)");
            int doors = Int32.Parse(Console.ReadLine());
            Engine engine = EngineMenu();
            Console.WriteLine("Color: white,black,gray,red,blue,yellow,burgundy");
            string color= Console.ReadLine();
            Vehicle car = new Car(engine,color,doors);
            this.controller=new Controller(car);
            Console.WriteLine("\n"+car.ToString());
            Console.WriteLine("Price:" +car.computePrice());
            Console.ReadKey();

        }
        public void MotorcycleMenu() {

            
            Console.WriteLine("\t----------------MOTORCYCLE----------------\n");
            Engine engine = EngineMenu();
            Console.WriteLine("Color: white,black,gray,red");
            string color = Console.ReadLine();
            Vehicle motorcycle = new Motorcycle(engine,color);
            this.controller = new Controller(motorcycle);
            Console.WriteLine("\n"+motorcycle.ToString());
            Console.WriteLine("Price:" + motorcycle.computePrice());
            Console.ReadKey();
        }

        public void TruckMenu() {

            Console.WriteLine("\t----------------TRUCK----------------\n");
            Engine engine = EngineMenu();
            Console.WriteLine("Color: white,black,gray,red");
            string color = Console.ReadLine();
            Console.WriteLine("No of wheels 4|6|8");
            int wheels= Int32.Parse(Console.ReadLine());
            Vehicle truck = new Truck(engine, color, wheels);
            this.controller=new Controller(truck);
            Console.WriteLine("\n"+truck.ToString());
            Console.WriteLine("Price:" + truck.computePrice());
            Console.ReadKey();

        }

        //@Paul PascalCase maybe?...in all project pls :D

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
