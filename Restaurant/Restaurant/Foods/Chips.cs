using System.Collections.Generic;

namespace Restaurant.Foods
{
    class Chips : Food
    {
        public override double CalculateHappiness(double happiness)
        {
            return happiness * 1.05;
        }

        public override string ToString()
        {
            return nameof(Chips);
        }
    }
}
