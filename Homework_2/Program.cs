using System;

namespace Homework_2
{
    internal class Program
    {
        static bool isPrime(int n)
        {
            if (n == 1 || n == 2) return true;
            else
            {
                for(int i = 2;i<= Math.Sqrt(n);i++)
                {
                    if (n % i == 0) return false;
                }
                return true;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Console.WriteLine("请输入上界：");
            int lower = int.Parse(Console.ReadLine());

            Console.WriteLine("请输入下界：");
            int upper = int.Parse(Console.ReadLine());

            int count = 0;
            for(int i = lower; i<=upper; i++)
            {
                if(isPrime(i))
                {
                    if (count == 10)
                    {
                        count = 0;
                        Console.WriteLine();
                    }
                    else count++;

                    Console.Write(i + " ");
                }
            }

            Console.WriteLine();
        }
    }
}
