namespace PizzaDecorator
{
    class SmallPizza : Pizza
    {
        public SmallPizza()
        {
            Name = "Small pizza: 28 cm";
            Price = 4.5;
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
