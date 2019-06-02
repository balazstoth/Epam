using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen
{
    class Oven
    {
        private Queue<Ingredient> cookingIngredients;

        public int Capacity { get; private set; }
        public string CurrentFoodType { get; set; }
        public bool IsOvenFree => Count == 0;
        public int Count => cookingIngredients.Count;
        public event OvenIsFinishedDelegate OvenIsFinishedEvent;

        public Oven(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(capacity.ToString());

            Capacity = capacity;
            CurrentFoodType = "Empty";
            cookingIngredients = new Queue<Ingredient>(capacity);
        }

        private void OnOvenIsFinished(OvenFinishedEventArgs e)
        {
            OvenIsFinishedDelegate eventHandler = OvenIsFinishedEvent;
            eventHandler?.Invoke(this, e);
        }
        private void Reset()
        {
            CurrentFoodType = "Empty";
            cookingIngredients.Clear();
        }

        public bool PlaceFoodToOven(IEnumerable<Ingredient> ingredients)
        {
            if (!IsOvenFree || ingredients == null || ingredients.Count() == 0 || ingredients.Count() > Capacity)
                return false;

            CurrentFoodType = ingredients.First().Name;
            foreach (Ingredient ingredient in ingredients)
            {
                if (ingredient.Name == CurrentFoodType && ingredient.NeedsCooking)
                    cookingIngredients.Enqueue(ingredient);
                else
                {
                    Reset();
                    return false;
                }
            }
            return true;
        }
        public async void CookIngredients()
        {
            Console.WriteLine($"Cooking started with {cookingIngredients.Count}x {cookingIngredients.First().Name}");
            await Task.Factory.StartNew(() => Thread.Sleep(cookingIngredients.Peek().CookingTime.Value));
            foreach (Ingredient ingredient in cookingIngredients)
                ingredient.IsCooked = true;
            Console.WriteLine($"{cookingIngredients.Count}x {cookingIngredients.First().Name} cooked");
            OnOvenIsFinished(new OvenFinishedEventArgs());
        }
        public IEnumerable<Ingredient> TakeFoodFromOven()
        {
            List<Ingredient> ready = new List<Ingredient>();
            while (cookingIngredients.Count > 0)
                ready.Add(cookingIngredients.Dequeue());

            Reset();
            return ready;
        }
    }
}
