using System.Collections.Generic;

namespace Restaurant.Foods
{
    class Chips : Food
    {

        public Chips(IEnumerable<Extra.Extra> extras = null)
            :base(extras)
        {
        }

        public override double CalculateHappiness(double happiness)
        {
            return happiness * 1.05;
        }
    }
}
