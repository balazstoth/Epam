using Kitchen.Ingredients;

namespace Kitchen.Foods
{
    class BasicHamburger : Food
    {
        public BasicHamburger()
            : base("Basic hamburger", new Ingredient[]
            {
                new Bun(), new Patty(), new Lettuce()
            })
        {
        }
    }
}
