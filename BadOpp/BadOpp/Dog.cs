using System;

namespace BadOpp
{
    class Dog : IAnimal
    {
        public int HairLength;
        private int noChildren;

        public void Run()
        {
            Console.Write("Dogs run fast.");
        }

        //it should have been abstract
        public bool HasChild()
        {
            return noChildren > 0;
        }
    }
}
