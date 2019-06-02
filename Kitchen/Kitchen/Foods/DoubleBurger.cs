using Kitchen.Ingredients;

namespace Kitchen.Foods
{
    class DoubleBurger : Food
    {
        public DoubleBurger()
            :base("Double hamburger", new Ingredient[]
            {
                new Bun(), new Patty(), new Patty(), new Lettuce(), new Tomato(), new Cheese(),  new Cheese(), new Ketchup()
            })
        {
        }
    }
}
