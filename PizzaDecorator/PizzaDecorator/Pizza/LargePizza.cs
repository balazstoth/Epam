namespace PizzaDecorator
{
    class LargePizza : Pizza
    {
        public LargePizza()
        {
            Name = "Large pizza: 40 cm";
            Price = 7.6;
        }

        public override double CalculatePrice()
        {
            return Price;
        }

        public override string GetName()
        {
            return Name;
        }
    }
}
