using Kitchen.Ingredients;

namespace Kitchen.Foods
{
    class CheeseBurger : Food
    {
        public CheeseBurger()
            :base("Cheeseburger", new Ingredient[] 
            {
                new Bun(), new Patty(), new Cheese(), new Ketchup()
            })
        {
        }
    }
}
