using System;
using System.Collections.Generic;

namespace Kitchen
{
    class Order
    {
        public List<Food> Foods { get; }

        public Order(List<Food> items)
        {
            if (items == null || items.Count == 0)
                throw new ArgumentException();

            Foods = new List<Food>();
            AddFoodToOrderRange(items);
        }

        public void AddFoodToOrder(Food food)
        {
            if (food == null)
                throw new ArgumentNullException(nameof(food));

            Foods.Add(food);
        }
        public void AddFoodToOrderRange(IEnumerable<Food> foods)
        {
            if (foods == null)
                throw new ArgumentNullException(nameof(foods));

            foreach (Food food in foods)
                AddFoodToOrder(food);
        }
    }
}
