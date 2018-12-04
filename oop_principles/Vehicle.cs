
namespace OopPrinciples
{
    abstract class Vehicle
    {
        private Engine engine;
        private string color;
        private int no_wheels;

       
        public Vehicle(Engine engine, string color, int no_wheels)
        {
            this.engine = engine;
            this.color = color;
            this.no_wheels = no_wheels; 
        }

        public abstract double ComputePrice();

        public string Color { get => color; set => color = value; }
        public int Wheels { get => no_wheels; set => no_wheels = value; }
        internal Engine Engine { get => engine; set => engine = value; }
    }
}
