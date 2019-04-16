using Restaurant.Foods;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Console.WriteLine("Order: Notifying observers of order {0}", this.ToString());
            FoodReady?.Invoke(new FoodReadyEventArgs(food));
            Console.WriteLine("Order: Notification done");
        }

        public Order(string food, IEnumerable<string> extras)
        {
            this.Food = food;
            this.Extras = extras;
        }

        public override string ToString()
        {
            return string.Format("[food={0}], extras=[{1}]", Food, string.Join(",",Extras));
        }
    }
}
