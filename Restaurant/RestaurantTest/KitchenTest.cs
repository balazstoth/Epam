using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurant.Foods;
using Restaurant;

namespace RestaurantTest
{
    [TestClass]
    public class KitchenTest
    {
        [TestMethod]
        public void Test_CreateMainFood(string foodName)
        {
            Kitchen kitchen = new Kitchen();
            Food expected;

            switch (foodName)
            {
                case "Chips":
                    expected = new Chips();
                    break;
                case "HotDog":
                    expected = new HotDog();
                    break;
                default:
                    expected = null;
                    break;
            }
        }
    }
}
