using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            Client c1 = new Client("Berci", 200);
            Client c2 = new Client("Peter", 100);

            Kitchen k1 = new Kitchen();
            Waitress waitress = new Waitress(k1);

            waitress.TakeOrder(c1, new Order("Chips", new List<string>() { "Mustard" }));
            waitress.TakeOrder(c2, new Order("HotDog", new List<string>() { "Ketchup" }));
            waitress.Start();

            Console.ReadKey();
        }
    }
}
