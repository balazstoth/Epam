using System.Collections.Generic;

namespace Restaurant.Foods
{
    public abstract class Food : IFood
    {
        public abstract double CalculateHappiness(double happiness);
    }
}
