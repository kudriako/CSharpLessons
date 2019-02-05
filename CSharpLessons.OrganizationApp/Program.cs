using System;
using System.Collections.Generic;
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

            var employeeArray = new IEmployee[] { alice, bruce, charlie };
            for(int i = 0; i < employeeArray.Length; i++)
            {
                Console.WriteLine(employeeArray[i]); // employees[i].ToString();
            }
            Console.WriteLine();
            Console.WriteLine();

            var employeeList = new List<IEmployee>() { alice, bruce, charlie };
            foreach(var employee in employeeList)
            {
                Console.WriteLine(employee.EmployeeCard);
            }
            Console.WriteLine();
            Console.WriteLine();

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
