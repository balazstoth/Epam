using Kitchen.Ingredients;

namespace Kitchen.Foods
{
    class NormalFF : Food
    {
        public NormalFF()
            :base("Normal French Fries", new Ingredient[]
            {
                new Fries()
            })
        {
        }
    }
}
