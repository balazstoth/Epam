using Restaurant.Foods;
using System;

namespace Restaurant
{
    public class FoodReadyEventArgs : EventArgs
    {
        public IFood food { get; }
        public FoodReadyEventArgs(IFood food)
        {
            food = food;
        }
    }
}
