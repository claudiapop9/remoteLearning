namespace BadOpp
{
    class Fish : IAnimal
    {
        private int noChildren;

        public bool Eat(string food)
        {
            return food.Equals("fish food");
        }

        //bad abstraction
        public void Run()
        {
            
        }
        //it should have been abstract
        public bool HasChild()
        {
            return noChildren > 0;
        }


    }
}
