using System.Collections.Generic;

namespace Restaurant.Foods
{
    class HotDog : Food
    {
        public HotDog(IEnumerable<Extra.Extra> extras = null)
             : base(extras)
        {
        }

        public override double CalculateHappiness(double happiness)
        {
            return happiness + 2;
        }
    }
}
