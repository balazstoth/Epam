using Restaurant.Foods;
using System.Collections.Generic;

namespace Restaurant
{
    public delegate void EventHandler(FoodReadyEventArgs foodReadyEventArgs);
    class Order
    {
        public event EventHandler FoodReady;
        public IEnumerable<string> Extras { get; }
        public string Food { get; }

        public void NotifyReady(IFood food)
        {
            FoodReady?.Invoke(new FoodReadyEventArgs(food));
        }

        public Order(string food, IEnumerable<string> extras)
        {
            this.Food = food;
            this.Extras = extras;
        }
    }
}
