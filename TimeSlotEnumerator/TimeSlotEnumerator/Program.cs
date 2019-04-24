using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSlotEnumerator
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSlot<string> ts = new TimeSlot<string>(4,5) { "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45"};

            foreach (var i in ts)
                Console.WriteLine(i);

            Console.ReadKey();
        }
    }
}
