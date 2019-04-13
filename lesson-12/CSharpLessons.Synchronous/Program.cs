using System;
using System.Diagnostics;

namespace CSharpLessons.Synchronous
{
    class Program
    {
        // Number 1000000000000003 is composite. Elapsed time 3,4465615 s.
        // Number 1000000000000037 is prime. Elapsed time 7,345191 s.
        // Number 100000000000000003 is prime. Elapsed time 73,8872612 s.
        // Number 100000000000000013 is prime. Elapsed time 73,9257551 s.
        static void Main(string[] args)
        {
            var checker = new PrimeNumberChecker();

            var input = string.Empty;
            while(true) 
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Enter the number:");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    break;
                if (!decimal.TryParse(input, out decimal number))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid Number.");
                    continue;
                }
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                bool isPrime = checker.IsPrime(number);
                stopwatch.Stop();
                if (isPrime)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Number {number} is prime. Elapsed time {stopwatch.Elapsed.TotalSeconds} s.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Number {number} is composite. Elapsed time {stopwatch.Elapsed.TotalSeconds} s.");
                }
                stopwatch.Reset();
            } 
        }

        static void CheckNumber(PrimeNumberChecker checker, int number)
        {
            
            Console.WriteLine($"Number {number} is prime;");
        }
    }
}
