using Restaurant.Foods;
using Restaurant.Foods.Extra;
using System;
using System.Collections.Generic;

namespace Restaurant
{
    public class Kitchen
    {
        private IFood AddExtras(IFood mainFood, IEnumerable<string> extras)
        {
            IFood food = mainFood;
            
            foreach (string item in extras)
            {
                if (item == typeof(Ketchup).Name)
                    food = new Ketchup(food);

                if (item == typeof(Mustard).Name)
                    food = new Mustard(food);
            }
            return food;
        }

        internal void Cook(Order order)
        {
            Console.WriteLine($"Kitchen: Preparing food, order: {order.ToString()}");
            IFood result = AddExtras(CreateMainFood(order.Food), order.Extras);
            Console.WriteLine($"Kitchen: Food prepared, food: {result.ToString()}");
            order.NotifyReady(result);
        }

        private IFood CreateMainFood(string food)
        {
            if (food == typeof(HotDog).Name)
                return new HotDog();

            return new Chips();
        }
    }
}
