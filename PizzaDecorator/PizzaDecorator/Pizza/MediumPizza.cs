namespace PizzaDecorator
{
    class MediumPizza : Pizza
    {
        public MediumPizza()
        {
            Name = "Medium pizza: 32 cm";
            Price = 6.0;
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
