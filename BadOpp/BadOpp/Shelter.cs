
namespace BadOpp
{
    class Shelter
    {
        private Bird bird=new Bird();
        private Dog dog = new Dog();

        public void isForbidden() {
            //bad encapsulation access field without method
            dog.HairLength = 10;
            System.Console.WriteLine("Shelter class");
        }
    }
}
