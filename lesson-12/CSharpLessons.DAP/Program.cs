using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace CSharpLessons.DAP
{
    class Program
    {
        // The delegate must have the same signature as the method
        // it will call asynchronously.
        public delegate bool AsyncMethodCaller(decimal number, out double milliseconds);

        // Number 1000000000000003 is composite. Elapsed time 3,4465615 s.
        // Number 1000000000000037 is prime. Elapsed time 7,345191 s.
        // Number 10000000000000037 is composite. Elapsed time 14,3157489 s.
        // Number 10000000000000069 is prime. Elapsed time 24,3446908 s
        // Number 10000000000000079 is prime. Elapsed time 24,3353885 s.
        // Number 100000000000000003 is prime. Elapsed time 73,8872612 s.
        // Number 100000000000000013 is prime. Elapsed time 73,9257551 s.
        static void Main(string[] args)
        {
            var checker = new PrimeNumberChecker();
            // Create the delegate.
            AsyncMethodCaller caller = new AsyncMethodCaller(checker.IsPrime);
            // Async results 
            var results = new List<IAsyncResult>();

            var input = string.Empty;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("--------");
                Console.WriteLine("Enter the number and press Enter (or press Enter for statistics or 'q' and Enter for exit):");
                input = Console.ReadLine();
                if (string.Equals(input, "q", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }
                if (string.IsNullOrEmpty(input))
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"Calculations started: {results.Count}");
                    Console.WriteLine($"Calculations acomplished: {results.Where(r => r.IsCompleted).Count()}");
                    Console.WriteLine($"Calculations in progress: {results.Where(r => !r.IsCompleted).Count()}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    continue;
                }
                if (!decimal.TryParse(input, out decimal number))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid number.");
                    continue;
                }
                // Last parameter is an object state
                IAsyncResult result = caller.BeginInvoke(number, out double seconds, new AsyncCallback(PrintResult), number);
                results.Add(result);
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Waiting for all calculations complete!");
            Console.ForegroundColor = ConsoleColor.Gray;
            // Wait for all results.
            WaitHandle[] handles = results.Select(r => r.AsyncWaitHandle).ToArray();
            if (handles.Length > 0)
            {
                WaitHandle.WaitAll(handles);
            }
        }

        static void PrintResult(IAsyncResult result)
        {
            AsyncResult asyncResult = (AsyncResult)result;
            AsyncMethodCaller caller = (AsyncMethodCaller)asyncResult.AsyncDelegate;
            // Retreaving previously stored number.
            decimal number = (decimal)result.AsyncState;
            try
            {
                // Call EndInvoke to retrieve the results.
                bool isPrime = caller.EndInvoke(out double seconds, result);
                if (isPrime)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Number {number} is prime. Elapsed time {seconds} s.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Number {number} is composite. Elapsed time {seconds} s.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            catch (Exception ex) // Exceptions from async operations will appear here.
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return;
            }
        }
    }
}
