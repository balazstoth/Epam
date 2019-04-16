using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Foods.Extra
{
    class Ketchup : Extra
    {
        public Ketchup(IFood food)
            :base(food)
        {
        }

        public override double CalculateHappiness(double happiness)
        {
            return _food.CalculateHappiness(happiness) + 
        }

        public override string ToString()
        {
            return string.Format($"{typeof(Ketchup).Name}[food={this._food.ToString()}]");
        }
    }
}
