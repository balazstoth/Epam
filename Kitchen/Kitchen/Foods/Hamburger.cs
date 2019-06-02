using Kitchen.Ingredients;

namespace Kitchen.Foods
{
    class NakedBurger : Food
    {
        public NakedBurger() 
            : base("Naked hamburger", new Ingredient[]
            {
                new Patty(), new Lettuce(), new Tomato(), new Ketchup()
            })
        {
        }
    }
}
