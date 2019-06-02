using Kitchen.Notify;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen
{
    class Kitchen
    {
        public Oven Oven { get; set; }
        public Order Order { get; set; }
        public Queue<Ingredient> IngredientsWaitingForPreparingQueue { get; set; }
        public List<Ingredient> PreparedIngredientsList { get; set; }
        public ObserveableQueue<Ingredient> PreparedIngredientsWaitForCookQueue { get; set; }
        public List<Food> FoodsReadyToServeList { get; set; } //Necessary?

        public Kitchen(Order order, Oven oven)
        {
            if (order == null || oven == null)
                throw new ArgumentException();

            IngredientsWaitingForPreparingQueue = new Queue<Ingredient>();
            PreparedIngredientsList = new List<Ingredient>();
            PreparedIngredientsWaitForCookQueue = new ObserveableQueue<Ingredient>();
            FoodsReadyToServeList = new List<Food>();
            Order = order;
            Oven = oven;
        }
    }
}
