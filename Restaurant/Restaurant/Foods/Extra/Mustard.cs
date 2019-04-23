using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Foods.Extra
{
    public class Mustard : Extra
    {
        public Mustard(IFood food)
            :base(food)
        {
        }

        public override double CalculateHappiness(double happiness)
        {
            return ++happiness;
        }

        public override string ToString()
        {
            return string.Format($"{nameof(Mustard)}[food={this._food.ToString()}]");
        }
    }
}
