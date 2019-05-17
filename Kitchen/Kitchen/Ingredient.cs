using System;

namespace Kitchen
{
    class Ingredient
    {
        public string Name { get; }
        public bool NeedsCooking { get; }
        public int PreparationTime { get; }
        public int? CookingTime { get; set; }

        public Ingredient(string name, bool needsCooking, int preparationTime, int? cookingTime)
        {
            CheckArguments(needsCooking, preparationTime, cookingTime);
            Name = name;
            NeedsCooking = needsCooking;
            PreparationTime = preparationTime;
            CookingTime = needsCooking == false ? null : cookingTime;
        }

        public override string ToString()
        {
            return Name;
        }
        private void CheckArguments(bool needsCooking, int preparationTime, int? cookingTime)
        {
            if(needsCooking)
            {
                if (cookingTime.HasValue == false)
                    throw new ArgumentNullException(nameof(cookingTime));

                if (cookingTime <= 0)
                    throw new ArgumentOutOfRangeException(cookingTime.ToString());
            }

            if (preparationTime <= 0)
                throw new ArgumentOutOfRangeException(cookingTime.ToString());
        }
    }
}
