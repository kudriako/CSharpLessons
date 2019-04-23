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
            // Example 1.
            // await ImmediateResultAsync();

            // Example 2
            //await WaitResultAsync(5);

            // Example 3
            //Console.WriteLine("Before asyn operation started.");
            //string url = await ReadFromFileAsync(@"D:\url.txt");
            //Console.WriteLine("After asyn operation started.");
            //Console.WriteLine("The url from file: {url}");
            //Console.WriteLine("After async operation complete.");

            // Example 3
            //string content = await ReadFromNetworkAsync(@"http://google.com");
            //Console.WriteLine(content.Substring(0, 80));

            // Example 4
            // var fileName = @"D:\url.txt";
            // var url = await ReadFromFileAsync(fileName);
            // var content = await ReadFromNetworkAsync(url);
            // return content;
            // Console.WriteLine(content.Substring(0, 80));

            // Example 5
            // var fileName = @"D:\url.txt";
            // var task = Task<string>.Run(async () => {
            //     var url = await ReadFromFileAsync(fileName);
            //     var content = await ReadFromNetworkAsync(url);
            //     return content;
            // }).ContinueWith(t => {
            //     Console.WriteLine(t.Result.Substring(0, 80));
            // });
            // task.Start();

            var fileName = @"D:\url.txt";
            var task = Task<string>.Run(async () => {
                var url = await ReadFromFileAsync(fileName);
                var content = await ReadFromNetworkAsync(url);
                return content;
            }).ContinueWith(t => {
                Console.WriteLine(t.Result.Substring(0, 80));
            });
            task.Start();

            /*string content = await ReadFromFileAsync(@"D:\url.txt").ContinueWith<string>(async (task) =>
            {
                return await ReadFromNetworkAsync("");
            });
            Console.WriteLine(content.Substring(0, 80));*/
        }

        static async Task<int> ImmediateResultAsync()
        {
            Console.WriteLine("ImmediateResultAsync");
            return await Task.FromResult(42);
        }

        static async Task WaitResultAsync(int seconds)
        {
            Console.WriteLine("WaitResultAsync");
            await Task.Delay(1000 * seconds);
        }

        static async Task<string> ReadFromFileAsync(string fileName)
        {
            Console.WriteLine("ReadFromFileAsync");
            using (var reader = File.OpenText(fileName))
            {
                var result = await reader.ReadToEndAsync();
                return result;
            }
        }

        static async Task<string> ReadFromNetworkAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                Console.WriteLine("ReadFromNetworkAsync");
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        static string ReadFromNetwork(string url)
        {
            using (WebClient client = new WebClient())
            {
                Console.WriteLine("ReadFromNetworkAsync");
                string responseBody = client.DownloadString(url);
                return responseBody;
            }
        }
    }
}
