using System;

namespace RationalNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Rational r1 = new Rational(-4, 5);
            Rational r2 = new Rational(2, 4);

            Console.WriteLine("R1 + R2: " + (r1 + r2));
            Console.WriteLine("R1 - R2: " + (r1 - r2));
            Console.WriteLine("R1 * R2: " + r1 * r2);
            Console.WriteLine("R1 / R2: " + r1 / r2);
            Console.WriteLine("-R1: " + -r1);
            Console.ReadKey();
        }
    }
}
