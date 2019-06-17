namespace PizzaDecorator
{
    abstract class Pizza
    {
        protected string Name { get; set; }
        protected double Price { get; set; }

        public abstract string GetName();
        public abstract double CalculatePrice();
    }
}
