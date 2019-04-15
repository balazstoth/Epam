using Restaurant.Foods;
using Restaurant.Foods.Extra;
using System.Collections.Generic;

namespace Restaurant
{
    class Kitchen
    {
        private IFood AddExtras(IFood mainFood, IEnumerable<string> extras)
        {
            Food food = mainFood as Food;
            food.extras = GetExtrasFromOrder(mainFood, extras);
            return food;
        }

        internal void Cook(Order order)
        {
            IFood result = AddExtras(CreateMainFood(order.Food), order.Extras);
            order.NotifyReady(result);
        }

        private IFood CreateMainFood(string food)
        {
            if (food == typeof(HotDog).Name)
                return new HotDog();

            return new Chips();
        }

        IEnumerable<Extra> GetExtrasFromOrder(IFood mainFood, IEnumerable<string> extras)
        {
            foreach (string i in extras)
            {
                if (i == typeof(Ketchup).Name)
                    yield return new Ketchup(mainFood);
                else
                    yield return new Mustard(mainFood);
            }
        }
    }
}
