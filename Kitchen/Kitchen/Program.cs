using Kitchen.Foods;
using System;
using System.Collections.Generic;

namespace Kitchen
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Initialize
            NakedBurger nakedBurger = new NakedBurger();
            BasicHamburger basicHamburger = new BasicHamburger();
            CheeseBurger cheeseBurger = new CheeseBurger();
            FullBurger fullBurger = new FullBurger();
            DoubleBurger doubleBurger = new DoubleBurger();
            NormalFF normalFF = new NormalFF();
            KetchupFF ketchupFF = new KetchupFF();

            Order order = new Order(new List<Food>()
            {
                basicHamburger,
                basicHamburger,
                cheeseBurger,
                cheeseBurger,
                doubleBurger,
                doubleBurger,
                fullBurger,
                fullBurger,
                ketchupFF,
                ketchupFF,
                ketchupFF,
                ketchupFF,
                normalFF,
                normalFF,
                normalFF
            });
            Oven oven = new Oven(4);
            Kitchen kitchen = new Kitchen(order, oven);
            Cook cook = new Cook(kitchen);
            #endregion

            cook.StartWorking();
            Console.ReadKey();
        }
    }
}
