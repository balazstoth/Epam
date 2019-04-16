using Restaurant.Foods;
using System;

namespace Restaurant
{
    public class FoodReadyEventArgs : EventArgs
    {
        public IFood Food { get; }
        public FoodReadyEventArgs(IFood food)
        {
            Food = food;
        }
    }
}
