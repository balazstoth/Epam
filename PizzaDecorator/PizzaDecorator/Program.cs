using System;

namespace PizzaDecorator
{
    class Program
    {
        static void Main(string[] args)
        {
            Pizza pizza = new Ham(new Cheese(new LargePizza()));
            Console.WriteLine(string.Join(", ", pizza.GetName(), pizza.CalculatePrice()) + "$");

            Console.ReadKey();
        }
    }
}
