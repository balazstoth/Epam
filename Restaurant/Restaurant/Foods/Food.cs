using System.Collections.Generic;

namespace Restaurant.Foods
{
    abstract class Food : IFood
    {
        public IEnumerable<Extra.Extra> extras { get; set; }

        public Food(IEnumerable<Extra.Extra> extras)
        {
            this.extras = extras;
        }

        public abstract double CalculateHappiness(double happiness);
    }
}
