using Kitchen.Ingredients;

namespace Kitchen.Foods
{
    class KetchupFF : Food
    {
        public KetchupFF()
            :base("French fries with ketchup", new Ingredient[]
            {
                new Fries(), new Ketchup()
            })
        {
        }
    }
}
