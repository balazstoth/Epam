using Kitchen.Ingredients;

namespace Kitchen.Foods
{
    class FullBurger : Food
    {
        public FullBurger() : base("Full hamburger", new Ingredient[]
            {
                new Bun(), new Patty(), new Lettuce(), new Tomato(), new Cheese(), new Ketchup()
            })
        {
        }
    }
}
