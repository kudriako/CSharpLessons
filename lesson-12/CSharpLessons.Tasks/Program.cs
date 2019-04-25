using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpLessons.Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);
            MainAsync().GetAwaiter().GetResult();
            Console.WriteLine(DateTime.Now);
        }

        static async Task MainAsync()
        {
            // Example 1. How return sync result from async method
            // await ImmediateResultAsync();

            // Example 2. How wait a time
            ///await WaitResultAsync(5);

            // Example 3
            // Console.WriteLine("Before asyn operation started.");
            // var task = Task<int>.Run(() => { return ReadFromFile(@"D:\url.txt");});
            // Console.WriteLine("After asyn operation started.");
            // Console.WriteLine($"The url from file: {task.Result}"); // or use // task.Wait();
            // Console.WriteLine("After async operation complete.");

            // Example 4
            //string url = await ReadFromFileAsync(@"D:\url.txt");
            //Console.WriteLine(url);

            // Example 5
            //string content = await ReadFromNetworkAsync(@"http://google.com");
            //Console.WriteLine(content.Substring(0, 80));

            // Example 6
            // var fileName = @"D:\url.txt";
            // Console.WriteLine("Before file read");
            // var url = await ReadFromFileAsync(fileName);
            // Console.WriteLine("After file read started");
            // var content = await ReadFromNetworkAsync(url);
            // Console.WriteLine("After network read started");
            // Console.WriteLine(content.Substring(0, 80));
            // Console.WriteLine("After network read complete");

            // Example 7
            // var fileName = @"D:\url.txt";
            // var task = Task<string>.Run(async () => {
            //     Console.WriteLine("Before file read");
            //     var url = await ReadFromFileAsync(fileName);
            //     Console.WriteLine("After file read started");
            //     var content = await ReadFromNetworkAsync(url);
            //     Console.WriteLine("After network read started");
            //     return content;
            // }).ContinueWith(t => {
            //     Console.WriteLine(t.Result.Substring(0, 80));
            //     Console.WriteLine("After network read complete");
            // });
            // await Task.Delay(100);
            // Console.WriteLine("In the main");
            // task.Wait();

            // Example 8
            // CancellationTokenSource source = new CancellationTokenSource();
            // CancellationToken token = source.Token;
            // var fileName = @"D:\url.txt";
            // var task = Task<string>.Run(async () =>
            // {
            //     var url = await ReadFromFileAsync(fileName);
            //     await Task.Delay(1000);
            //     if (token.IsCancellationRequested)
            //     {
            //         token.ThrowIfCancellationRequested();
            //     }
            //     await Task.Delay(1000);
            //     var content = await ReadFromNetworkAsync(url);
            //     return content;
            // }, token).ContinueWith(t =>
            // {
            //     if (token.IsCancellationRequested)
            //     {
            //         token.ThrowIfCancellationRequested();
            //     }
            //     Console.WriteLine($"[{Process.GetCurrentProcess().Threads.Count}] {t.Result.Substring(0, 80)}");
            // });
            // source.CancelAfter(1000);
            // try 
            // {
            //     task.Wait();
            // }
            // catch(Exception ex)
            // {
            //     Console.Write(ex);
            // }
            // Console.WriteLine(task.IsCanceled);

            // Example 9
            // var progress = new Progress<int>();
            // progress.ProgressChanged += (sender, count) => {
            //     Console.WriteLine($"Progress {count}");
            // };
            // var fileName = @"D:\url.txt";
            // var task = Task<string>.Run(async () => {
            //     ((IProgress<int>)progress).Report(0);
            //     var url = await ReadFromFileAsync(fileName);
            //     ((IProgress<int>)progress).Report(50);
            //     var content = await ReadFromNetworkAsync(url);
            //     ((IProgress<int>)progress).Report(100);
            //     return content;
            // }).ContinueWith(t => {
            //     Console.WriteLine(t.Result.Substring(0, 80));
            // });
            // task.ConfigureAwait(true);
            // task.Wait();
        }

        static async Task<int> ImmediateResultAsync()
        {
            Console.WriteLine($"[{Process.GetCurrentProcess().Threads.Count}] ImmediateResultAsync");
            return await Task.FromResult(42);
        }

        static async Task WaitResultAsync(int seconds)
        {
            Console.WriteLine($"[{Process.GetCurrentProcess().Threads.Count}] WaitResultAsync");
            await Task.Delay(1000 * seconds);
        }

        static string ReadFromFile(string fileName)
        {
            Console.WriteLine($"[{Process.GetCurrentProcess().Threads.Count}] ReadFromFile");
            using (var reader = File.OpenText(fileName))
            {
                var result = reader.ReadToEnd();
                return result;
            }
        }

        static async Task<string> ReadFromFileAsync(string fileName)
        {
            using (var reader = File.OpenText(fileName))
            {
                var result = await reader.ReadToEndAsync();
                Console.WriteLine($"[{Process.GetCurrentProcess().Threads.Count}] ReadFromFileAsync");
                return result;
            }
        }

        static async Task<string> ReadFromNetworkAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                Console.WriteLine($"[{Process.GetCurrentProcess().Threads.Count}] ReadFromNetworkAsync");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        static string ReadFromNetwork(string url)
        {
            using (WebClient client = new WebClient())
            {
                Console.WriteLine($"[{Process.GetCurrentProcess().Threads.Count}] ReadFromNetworkAsync");
                string responseBody = client.DownloadString(url);
                return responseBody;
            }
        }
    }
}
