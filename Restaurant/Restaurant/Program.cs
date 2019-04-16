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
            Client clien1 = new Client("Berci", 200);
            Client client2 = new Client("Peter", 100);

            Kitchen kitchen = new Kitchen();
            Waitress waitress = new Waitress(kitchen);

            waitress.TakeOrder(clien1, new Order("Chips", new List<string>() { "Mustard" }));
            waitress.TakeOrder(client2, new Order("HotDog", new List<string>(){ "Ketchup" }));
            waitress.Start();

            Console.ReadKey();
        }
    }
}
