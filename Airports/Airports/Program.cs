using System;


namespace Airports
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!FileCheck.FileExist())
            {
                Transform tsfm = new Transform();
                tsfm.StartTransform();
            }
            Console.WriteLine("Ready");
            Console.ReadKey();
        }
    }
}
