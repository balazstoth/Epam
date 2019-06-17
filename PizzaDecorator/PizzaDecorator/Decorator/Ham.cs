namespace PizzaDecorator
{
    class Ham : PizzaDecorator
    {
        public Ham(Pizza pizza)
            :base(pizza)
        {
            Name = "Ham";
            Price = 1.9;
        }

        public override double CalculatePrice()
        {
            return Price + pizza.CalculatePrice();
        }

        public override string GetName()
        {
            return pizza.GetName() + " + " + Name;
        }
    }
}
