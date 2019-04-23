using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurant;
using Restaurant.Foods;
using Restaurant.Foods.Extra;

namespace RestaurantTest
{
    [TestClass]
    public class ClientTest
    {
        [DataRow(-5)]
        [DataRow(-8.45)]
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_CreateClient(double happiness)
        {
            Client client = new Client("", happiness);
        }


        [DataRow(0)]
        [DataRow(4)]
        [DataRow(3.43)]
        [DataRow(100)]
        [DataRow(13121)]
        [TestMethod]
        public void Test_CalculateHappiness_HotDog(double happiness)
        {
            Client client = new Client("", happiness);
            HotDog hotdog = new HotDog();

            client.Eat(hotdog);
            var expected = happiness + 2;
            Assert.AreEqual(expected, client.Happiness);
        }


        [DataRow(0)]
        [DataRow(14)]
        [DataRow(3.43)]
        [DataRow(100)]
        [DataRow(13121)]
        [TestMethod]
        public void Test_CalculateHappiness_HotdogWithKetchup(double happiness)
        {
            Client client = new Client("", happiness);
            HotDog hotDog = new HotDog();
            Ketchup ketchup = new Ketchup(hotDog);

            client.Eat(ketchup);
            var expected = happiness + 4;
            Assert.AreEqual(expected, client.Happiness);
        }

        [DataRow(0)]
        [DataRow(14)]
        [DataRow(3.43)]
        [DataRow(100)]
        [DataRow(13121)]
        [TestMethod]
        public void Test_CalculateHappiness_HotdogWithMustard(double happiness)
        {
            Client client = new Client("", happiness);
            HotDog hotDog = new HotDog();
            Mustard mustard = new Mustard(hotDog);

            client.Eat(mustard);
            var expected = happiness + 1;
            Assert.AreEqual(expected, client.Happiness);
        }

        [DataRow(0)]
        [DataRow(14)]
        [DataRow(3.43)]
        [DataRow(100)]
        [DataRow(13121)]
        [TestMethod]
        public void Test_CalculateHappiness_Chips(double happiness)
        {
            Client client = new Client("", happiness);
            Chips chips = new Chips();

            client.Eat(chips);
            var expected = happiness * 1.05;
            Assert.AreEqual(expected, client.Happiness);
        }

        [DataRow(0)]
        [DataRow(14)]
        [DataRow(3.43)]
        [DataRow(100)]
        [DataRow(13121)]
        [TestMethod]
        public void Test_CalculateHappiness_ChipsWithKetchup(double happiness)
        {
            Client client = new Client("", happiness);
            Chips chips = new Chips();
            Ketchup ketchup = new Ketchup(chips);

            client.Eat(ketchup);
            var expected = happiness * 1.05 * 1.05;
            Assert.AreEqual(expected, client.Happiness);
        }

        [DataRow(0)]
        [DataRow(14)]
        [DataRow(3.43)]
        [DataRow(100)]
        [DataRow(13121)]
        [TestMethod]
        public void Test_CalculateHappiness_ChipsWithMustard(double happiness)
        {
            Client client = new Client("", happiness);
            Chips chips = new Chips();
            Mustard mustard = new Mustard(chips);

            client.Eat(mustard);
            var expected = happiness + 1;
            Assert.AreEqual(expected, client.Happiness);
        }
    }
}
