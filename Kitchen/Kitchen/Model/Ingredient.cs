using System;

namespace Kitchen
{
    abstract class Ingredient
    {
        private bool isCooked;
        private bool isPrepared;

        public string Name { get; }
        public bool NeedsCooking { get; }
        public int PreparationTime { get; }
        public int? CookingTime { get; }
        public bool IsCooked
        {
            get => isCooked;
            set
            {
                isPrepared = value;
                CheckOrderState();
            }
        }
        public bool IsPrepared
        {
            get => isPrepared;
            set
            {
                isPrepared = value;
                CheckOrderState();
            }
        }
        public bool IsReady { get; set; }

        public Ingredient(string name, bool needsCooking, int preparationTime, int? cookingTime)
        {
            CheckArguments(needsCooking, preparationTime, cookingTime);
            Name = name;
            NeedsCooking = needsCooking;
            PreparationTime = preparationTime;
            CookingTime = needsCooking == false ? null : cookingTime;
        }
        private void CheckArguments(bool needsCooking, int preparationTime, int? cookingTime)
        {
            if (needsCooking)
            {
                if (!cookingTime.HasValue)
                    throw new ArgumentNullException(nameof(cookingTime));

                if (cookingTime <= 0)
                    throw new ArgumentOutOfRangeException(cookingTime.ToString());
            }

            if (preparationTime < 0)
                throw new ArgumentOutOfRangeException(cookingTime.ToString());
        }
        public override string ToString()
        {
            return Name;
        }

        private void CheckOrderState()
        {
            if (NeedsCooking)
                IsReady = IsCooked && IsPrepared;
            else
                IsReady = IsPrepared;
        }
    }
}
