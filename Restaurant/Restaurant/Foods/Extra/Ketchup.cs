﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Foods.Extra
{
    public class Ketchup : Extra
    {
        public Ketchup(IFood food)
            :base(food)
        {
        }

        public override double CalculateHappiness(double happiness)
        {
            return _food.CalculateHappiness(_food.CalculateHappiness(happiness));
        }

        public override string ToString()
        {
            return string.Format($"{nameof(Ketchup)}[food={this._food.ToString()}]");
        }
    }
}
