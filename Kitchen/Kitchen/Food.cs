using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen
{
    class Food
    {
        public string Name { get; }
        public Ingredient[] Ingredients { get; }

        public Food(string name, Ingredient[] ingredients)
        {
            if (ingredients == null || ingredients.Length == 0)
                throw new ArgumentException(nameof(ingredients));

            Name = name;
            Ingredients = ingredients;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
