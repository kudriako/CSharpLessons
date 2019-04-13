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
        // Number 10000000000000069 is prime. Elapsed time 24,8230188 s.
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
                // Last parameter is an object state
                IAsyncResult result = caller.BeginInvoke(number, out double seconds, new AsyncCallback(PrintResult), number);
                results.Add(result);
                Console.WriteLine($"Calculations started: {results.Count}");
                Console.WriteLine($"Calculations complete: {results.Where(r => r.IsCompleted).Count()}");
                Console.WriteLine($"Calculations in progress: {results.Where(r => !r.IsCompleted).Count()}");
            }

            // Wait for all results.
            WaitHandle[] handles = results.Select(r => r.AsyncWaitHandle).ToArray();
            WaitHandle.WaitAll(handles);
        }

        static void PrintResult(IAsyncResult result)
        {
            AsyncResult asyncResult = (AsyncResult)result;
            AsyncMethodCaller caller = (AsyncMethodCaller)asyncResult.AsyncDelegate;
            decimal number = (decimal)result.AsyncState;
            // Call EndInvoke to retrieve the results.
            try
            {
                bool isPrime = caller.EndInvoke(out double seconds, result);
                if (isPrime)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Number {number} is prime. Elapsed time {seconds} s.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Number {number} is composite. Elapsed time {seconds} s.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

        }
    }
}
