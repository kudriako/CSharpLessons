using System;
using CSharpLessons.OrganizationModel;

namespace CSharpLessons.OrganizationApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Start();

            var alice = new Employee() { Name = "Alice", Title = "Test Engineer" };

            var bruce = new Employee() { Name = "Bruce", Title = "Developer" };

            var charlie = new Manager() { Name = "Charlie", Title = "Program Manager" };
            alice.Manager = charlie;
            bruce.Manager = charlie;

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
