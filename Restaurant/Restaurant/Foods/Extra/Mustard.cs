﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Foods.Extra
{
    class Mustard : Extra
    {
        public Mustard(IFood food)
            :base(food)
        {
        }

        public override double CalculateHappiness(double happiness)
        {
        }
    }
}