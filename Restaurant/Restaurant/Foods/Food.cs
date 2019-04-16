using System.Collections.Generic;

namespace Restaurant.Foods
{
    abstract class Food : IFood
    {
        public abstract double CalculateHappiness(double happiness);
    }
}
