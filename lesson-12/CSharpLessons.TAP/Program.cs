using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpLessons.TAP
{
    class Program
    {
        // Number 1000000000000003 is composite. Elapsed time 3,4465615 s.
        // Number 1000000000000037 is prime. Elapsed time 7,0797748 s.
        // Number 10000000000000037 is composite. Elapsed time 14,3157489 s.
        // Number 10000000000000069 is prime. Elapsed time 24,3446908 s
        // Number 10000000000000079 is prime. Elapsed time 24,3353885 s.
        // Number 100000000000000003 is prime. Elapsed time 73,8872612 s.
        // Number 100000000000000013 is prime. Elapsed time 73,9257551 s.

        static object syncRoot = new object();

        //Still synchronous. Uncomment second Main for async.
        static void Main(string[] args)
        {
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
                var cancellationToken = new CancellationToken();
                var task = IsPrime(number, cancellationToken);
                task.Wait();
                var result = task.Result;
                if (result.IsPrime)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Number {result.Number} is prime. Elapsed time {result.Seconds} s.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Number {result.Number} is composite. Elapsed time {result.Seconds} s.");
                }
            }
        }

        private static ConcurrentBag<Task<PrimeNumberResult>> _tasks = new ConcurrentBag<Task<PrimeNumberResult>>();

        // static void Main(string[] args)
        // {
        //     var input = string.Empty;
        //     var cancellationSource = new CancellationTokenSource();
        //     var cancellationToken = cancellationSource.Token;
        //     var queueTask = StartQueue(cancellationToken);

        //     var allTasks = new List<Task<PrimeNumberResult>>();

        //     while (!queueTask.IsCanceled)
        //     {
        //         Console.ForegroundColor = ConsoleColor.Gray;
        //         Console.WriteLine("Enter the number:");
        //         input = Console.ReadLine();
        //         if (string.Equals(input, "q", StringComparison.InvariantCultureIgnoreCase))
        //         {
        //             cancellationSource.Cancel();
        //             break;
        //         }
        //         if (string.IsNullOrEmpty(input))
        //         {
        //             Console.WriteLine(new String('#', 20));
        //             foreach (var item in allTasks)
        //             {
        //                 DisplayTaskResults(item, true);
        //             }
        //             continue;
        //         }
        //         if (!decimal.TryParse(input, out decimal number))
        //         {
        //             Console.ForegroundColor = ConsoleColor.Red;
        //             Console.WriteLine($"Invalid Number.");
        //             continue;
        //         }
        //         var task = IsPrime(number, cancellationToken);
        //         _tasks.Add(task);
        //         allTasks.Add(task);
        //         Console.ForegroundColor = ConsoleColor.White;
        //         Console.WriteLine($"Task {task.Id} started for number {number}");
        //         Console.ForegroundColor = ConsoleColor.Gray;
        //         Thread.Sleep(0);
        //     }

        //     // Cancellation awaiting 
        //     // try
        //     // {
        //     //     Task.WaitAll(allTasks.ToArray());
        //     // }
        //     // catch (AggregateException)
        //     // {
        //     //     // Some cancellation exceptions may be here
        //     // }

        //     Console.WriteLine(new String('#', 20));
        //     foreach (var task in allTasks)
        //     {
        //         DisplayTaskResults(task, true);
        //     }
        // }

        public static Task<PrimeNumberResult> IsPrime(decimal number, CancellationToken token)
        {
            // if (number < 1)
            // {
            //     // Early exception
            //     throw new InvalidOperationException($"{number} is invalid number.");
            // }
            return Task.Run(() =>
            {

                if (number < 1)
                {
                    // Aggregate exception
                    throw new InvalidOperationException($"{number} is invalid number.");
                }
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                if (number == 2)
                    return new PrimeNumberResult(number, true, stopwatch);
                if (number % 2 == 0)
                    return new PrimeNumberResult(number, false, stopwatch);
                var limit = (uint)Math.Sqrt((double)number);
                for (uint i = 3; i <= limit; i += 2)
                {
                    if (number % i == 0)
                        return new PrimeNumberResult(number, false, stopwatch);
                    if (token.IsCancellationRequested)
                        token.ThrowIfCancellationRequested();
                }
                return new PrimeNumberResult(number, true, stopwatch);
            }, token);
        }

        public static Task<PrimeNumberResult> IsPrime2(decimal number, CancellationToken token)
        {
            var task = new Task<PrimeNumberResult>(() => 
            {
                if (number < 1)
                {
                    // Aggregate exception
                    throw new InvalidOperationException($"{number} is invalid number.");
                }
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                if (number == 2)
                    return new PrimeNumberResult(number, true, stopwatch);
                if (number % 2 == 0)
                    return new PrimeNumberResult(number, false, stopwatch);
                var limit = (uint)Math.Sqrt((double)number);
                for (uint i = 3; i <= limit; i += 2)
                {
                    if (number % i == 0)
                        return new PrimeNumberResult(number, false, stopwatch);
                    if (token.IsCancellationRequested)
                        token.ThrowIfCancellationRequested();
                }
                return new PrimeNumberResult(number, true, stopwatch);
            });
            task.ConfigureAwait(true);
            task.Start();
            return task;
        }

        public static Task StartQueue(CancellationToken token)
        {
            return Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (_tasks.Any())
                    {
                        var tasksToRemove = new List<Task<PrimeNumberResult>>();
                        Task.WaitAny(_tasks.ToArray(), 1000);
                        Console.WriteLine(new String('-', 20));
                        foreach (var task in _tasks)
                        {
                            DisplayTaskResults(task);
                            if (task.IsCompleted)
                            {
                                tasksToRemove.Add(task);
                            }
                        }
                        tasksToRemove.ForEach(t => _tasks.TryTake(out Task<PrimeNumberResult> removed));
                    }
                    Task.Delay(1000);
                }
            });
        }

        private static void DisplayTaskResults(Task<PrimeNumberResult> task, bool displayInProgress = false)
        {
            if (task.IsCanceled)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"#{task.Id.ToString().PadRight(8)} - Task was cancelled.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (task.IsFaulted)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"#{task.Id.ToString().PadRight(8)} - Task is Fault: {task.Exception.Flatten().InnerExceptions.FirstOrDefault()?.Message}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (task.IsCompleted)
            {
                if (task.Result.IsPrime)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"#{task.Id.ToString().PadRight(8)} - {task.Result.Number.ToString().PadRight(20)}: Prime. Elapsed time {task.Result.Seconds} s.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"#{task.Id.ToString().PadRight(8)} - {task.Result.Number.ToString().PadRight(20)}: Composite. Elapsed time {task.Result.Seconds} s.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            else if (displayInProgress)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"#{task.Id.ToString().PadRight(8)} - Still in progress.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
