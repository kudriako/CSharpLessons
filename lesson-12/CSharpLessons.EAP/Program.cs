using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace CSharpLessons.EAP
{
    class Program
    {
        // Number 1000000000000003 is composite. Elapsed time 3,4465615 s.
        // Number 1000000000000037 is prime. Elapsed time 7,345191 s.
        // Number 10000000000000037 is composite. Elapsed time 14,3157489 s.
        // Number 10000000000000069 is prime. Elapsed time 24,3446908 s
        // Number 10000000000000079 is prime. Elapsed time 24,3353885 s.
        // Number 100000000000000003 is prime. Elapsed time 73,8872612 s.
        // Number 100000000000000013 is prime. Elapsed time 73,9257551 s.

        static object syncRoot = new object();

        static void Main(string[] args)
        {
            var checker = new PrimeNumberChecker();
            checker.ProgressChanged += PrintProgress;
            checker.Completed += PrintResult;
            var statesInProgress = new HashSet<object>();
            var input = string.Empty;
            while (true)
            {
                //lock (syncRoot)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("--------");
                    Console.WriteLine("Enter the number and press Enter (or press Enter for statistics or 'q' and Enter for exit or 't' to terminate calculations):");
                    input = Console.ReadLine();
                    if (string.Equals(input, "q", StringComparison.InvariantCultureIgnoreCase))
                    {
                        break;
                    }
                    if (string.Equals(input, "t", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"Terminating calculations!");
                        foreach (var item in statesInProgress)
                        {
                            checker.CancelAsync(item);
                        }
                        statesInProgress.Clear();
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        continue;
                    }
                    if (!decimal.TryParse(input, out decimal number))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Invalid number.");
                        continue;
                    }
                    object state = (object)number; // boxed decimal becames an object and we can use it as userSuppliedState (identifier).
                    if (!statesInProgress.Add(state))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Number is already added.");
                        continue;
                    }
                    // Last parameter is an object state
                    checker.IsPrimeAsync(number, state);
                    statesInProgress.Add(state);
                }
            }
            //Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void PrintProgress(object sender, PrimeNumberCheckProgressChangedEventArgs args)
        {
            //lock (syncRoot)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                var numberString = args.Number.ToString().PadRight(20);
                var progress = args.ProgressPercentage / 5;
                var calculatedPad = new String('*', progress);
                var remainingPad = new String('-', 20 - progress);
                Console.WriteLine($"{numberString} [{calculatedPad}{remainingPad}] ({args.Seconds} s.)");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        static void PrintResult(object sender, PrimeNumberCheckCompletedEventArgs args)
        {
            //lock (syncRoot)
            {
                if (args.Error != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(args.Error.Message);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if (args.Cancelled)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"Number {args.Number} calculation is cancelled");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if (args.IsPrime)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Number {args.Number} is prime. Elapsed time {args.Seconds} s.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Number {args.Number} is composite. Elapsed time {args.Seconds} s.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
        }
    }
}
