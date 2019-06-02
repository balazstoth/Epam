using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Kitchen
{
    class Cook
    {
        private Kitchen kitchen;
        private Order Order
        {
            get { return kitchen.Order; }
            set { kitchen.Order = value; }
        }
        private Oven Oven
        {
            get { return kitchen.Oven; }
            set { kitchen.Oven = value; }
        }

        private bool AreIngredientsToTakeFromOven { get; set; }

        public Cook(Kitchen kitchen)
        {
            this.kitchen = kitchen ?? throw new ArgumentException();
            Oven.OvenIsFinishedEvent += NotifyOvenHasFinished;
            AreIngredientsToTakeFromOven = false;
        }

        public void StartWorking()
        {
            SeparateIngredients();
            StartPreparing();
        }
        private void StartPreparing()
        {
            Ingredient currentIngredient;
            while (kitchen.IngredientsWaitingForPreparingQueue.Count > 0)
            {
                currentIngredient = PrepareIngredient(kitchen.IngredientsWaitingForPreparingQueue.Dequeue());
                if (currentIngredient.NeedsCooking)
                    kitchen.PreparedIngredientsWaitForCookQueue.Enqueue(currentIngredient);
                else
                    kitchen.PreparedIngredientsList.Add(currentIngredient);

                CheckIfOvenHasFinished();
                StartOvenIfPossible();
            }

            MakeFoodsFromIngredients();
        }
        private void MakeFoodsFromIngredients()
        {
            while (!OrderIsCompleted())
            {
                CheckIfOvenHasFinished();
                StartOvenIfPossible();
                TryMakingFood();
            }
        }
        private void TryMakingFood()
        {
            foreach (Food food in Order.Foods)
            {
                bool found = true;
                var groupedIngredients = food.Ingredients.GroupBy(i => i.Name).Select(i => new { Name = i.Key, Count = i.Count() }).ToDictionary(k => k.Name, v => v.Count);
                foreach (var ingredient in groupedIngredients)
                    if (!IsIngredientReady(ingredient.Key, ingredient.Value))
                    {
                        found = false;
                        break;
                    }

                if (!found)
                    continue;

                foreach (var ingredient in groupedIngredients)
                    UseIngredientsToCreateFood(ingredient.Key, ingredient.Value);

                Console.WriteLine($"1x {food.Name} served");
                kitchen.FoodsReadyToServeList.Add(food);
            }
        }
        private void UseIngredientsToCreateFood(string name, int quantity)
        {
            var current = kitchen.PreparedIngredientsList.Where(x => x.Name == name).First();

            for (int i = 0; i < quantity; i++)
                kitchen.PreparedIngredientsList.Remove(current);
        }
        private bool IsIngredientReady(string name, int quantity)
        {
            int count = 0;
            int i = 0;

            while (count < quantity && i < kitchen.PreparedIngredientsList.Count)
            {
                if (kitchen.PreparedIngredientsList[i++].Name == name)
                    count++;
            }
            return count >= quantity;
        }
        private void StartOvenIfPossible()
        {
            var nextIngredient = kitchen.PreparedIngredientsWaitForCookQueue.Peek();
            if (CanStartOven(nextIngredient))
            {
                int count = 0;
                List<Ingredient> readyToCook = new List<Ingredient>();
                string type = kitchen.PreparedIngredientsWaitForCookQueue.Peek().Name;
                while (kitchen.PreparedIngredientsWaitForCookQueue.Count > 0 && count < Oven.Capacity && type == kitchen.PreparedIngredientsWaitForCookQueue.Peek().Name)
                {
                    readyToCook.Add(kitchen.PreparedIngredientsWaitForCookQueue.Dequeue());
                    count++;
                }
                Oven.PlaceFoodToOven(readyToCook);
                Oven.CookIngredients();
            }
        }
        private void CheckIfOvenHasFinished()
        {
            if (AreIngredientsToTakeFromOven)
            {
                var ingredients = Oven.TakeFoodFromOven();
                foreach (Ingredient ingredient in ingredients)
                    kitchen.PreparedIngredientsList.Add(ingredient);
                AreIngredientsToTakeFromOven = false;
            }
        }
        private bool CanStartOven(Ingredient next)
        {
            return Oven.IsOvenFree && 
                (kitchen.PreparedIngredientsWaitForCookQueue.Count >= Oven.Capacity || 
                (next == null && kitchen.PreparedIngredientsWaitForCookQueue.Count > 0) || 
                (next != null && next.Name != Oven.CurrentFoodType && Oven.CurrentFoodType != "Empty" && kitchen.PreparedIngredientsWaitForCookQueue.Count > 0));
        }
        private Ingredient PrepareIngredient(Ingredient ingredient)
        {
            Thread.Sleep(ingredient.PreparationTime);
            ingredient.IsPrepared = true;
            if (ingredient.NeedsCooking)
                Console.WriteLine($"{ingredient.Name} prepared for cooking");
            else
                Console.WriteLine($"{ingredient.Name} prepared");

            return ingredient;
        }
        private void NotifyOvenHasFinished(object sender, OvenFinishedEventArgs e)
        {
            AreIngredientsToTakeFromOven = true;
        }
        private void SeparateIngredients()
        {
            var ingredients = Order.Foods.SelectMany(f => f.Ingredients).OrderByDescending(i => i.NeedsCooking).ToList();
            foreach (Ingredient item in ingredients)
                kitchen.IngredientsWaitingForPreparingQueue.Enqueue(item);
        }
        private bool OrderIsCompleted()
        {
            return kitchen.Order.Foods.Count == kitchen.FoodsReadyToServeList.Count();
        }
    }
}
