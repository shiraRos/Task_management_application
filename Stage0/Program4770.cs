using System;
namespace Targil0
{
    partial class Program
    {
       
        static partial void Welcome6836();

        private static void Welcome4770()
        {
            string nm;
            Console.WriteLine("Enter your name:");
            nm = Console.ReadLine();
            Console.WriteLine("{0}, wellcome to my first console application", nm);
        }
        static void Main(string[] args)
        {
            Welcome4770();
            Welcome6836();
            Console.ReadKey();
        }
    }
}