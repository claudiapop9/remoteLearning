namespace BadOpp
{
    internal class Bird : IAnimal, IMammal //bad inheritance
    {
        private int noChildren;

        public bool Eat(string food)
        {
            return food.Equals("bird food");
        }
        //bad abstraction
        void IAnimal.Run()
        {

        }

        //it should have been abstract
        public bool HasChild()
        {
            return noChildren > 0;
        }
    }
}
