using Restaurant.Foods;
using System;

namespace Restaurant
{
    class Client
    {
        public double Happiness { get; set; }
        public string Name { get; }
        public Client(string name, double happiness)
        {
            Name = name;
            Happiness = happiness;
        }

        public void Eat(FoodReadyEventArgs foodReadyEventArgs)
        {
            Console.WriteLine("Starting to eat food, client: {0}, food: {1}", this.ToString(), foodReadyEventArgs.ToString());
            Console.WriteLine("Nyam nyam");
            Happiness = foodReadyEventArgs.food.CalculateHappiness(Happiness);
            Console.WriteLine("Food eaten, client: ", this.ToString());
        }

        internal void Eat(IFood food)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Format("[name={0}, happiness={1}]",Name,Happiness.ToString("F2"));
        }
    }
}
