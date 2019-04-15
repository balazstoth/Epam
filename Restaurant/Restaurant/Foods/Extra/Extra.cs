using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Foods.Extra
{
    abstract class Extra : IFood
    {
        protected IFood _food;
        public Extra(IFood food)
        {
            _food = food;
        }

        public abstract double CalculateHappiness(double happiness);
    }
}
