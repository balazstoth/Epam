namespace PizzaDecorator
{
    class Cheese : PizzaDecorator
    {
        public Cheese(Pizza pizza)
            :base(pizza)
        {
            Name = "Cheese";
            Price = 1.0;
        }

        public override string GetName()
        {
            return pizza.GetName() + " + " + Name;
        }

        public override double CalculatePrice()
        {
            return Price + pizza.CalculatePrice();
        }
    }
}
