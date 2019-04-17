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

        public void Eat(IFood food)
        {
            Console.WriteLine("Client: Starting to eat food, client: {0}, food: {1}", this.ToString(), food.ToString());
            Console.WriteLine("Client: Nyam nyam");
            Happiness = food.CalculateHappiness(Happiness);
            Console.WriteLine("Client: Food eaten, client: {0}", this.ToString());
        }

        public override string ToString()
        {
            return string.Format("[name={0}, happiness={1}]",Name,Happiness.ToString("F1"));
        }
    }
}
