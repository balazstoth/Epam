using System;
using System.Collections.Generic;

namespace Kitchen
{
    class Order
    {
        public List<Food> OrderedFoods { get; }

        public Order(Food firstItem)
        {
            OrderedFoods = new List<Food>();
            AddFoodToOrder(firstItem);
        }

        public void AddFoodToOrder(Food food)
        {
            if (food == null)
                throw new ArgumentNullException(nameof(food));

            OrderedFoods.Add(food);
        }
        public void AddFoodToOrderRange(IEnumerable<Food> foods)
        {
            foreach (Food food in foods)
                AddFoodToOrder(food);
        }
    }
}
