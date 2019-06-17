namespace PizzaDecorator
{
    class PizzaDecorator : Pizza
    {
        protected Pizza pizza;

        public PizzaDecorator(Pizza pizza)
        {
            this.pizza = pizza;
        }

        public override double CalculatePrice()
        {
            return pizza.CalculatePrice();
        }

        public override string GetName()
        {
            return pizza.GetName();
        }
    }
}
