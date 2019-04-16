using System.Collections.Generic;

namespace Restaurant.Foods
{
    class HotDog : Food
    {
        public override double CalculateHappiness(double happiness)
        {
            return happiness + 2;
        }

        public override string ToString()
        {
            return typeof(HotDog).Name;
        }
    }
}
