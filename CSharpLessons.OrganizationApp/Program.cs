using System;

namespace CSharpLessons.OrganizationApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Start();
            End();
        }

        static void Start()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void End()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
