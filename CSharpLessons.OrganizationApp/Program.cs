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

            var chloe = new Manager() { Name = "Chloe", Title = "Program Manager" };
            alice.Manager = chloe;
            bruce.Manager = chloe;
            chloe.Employees.Add(alice);
            chloe.Employees.Add(bruce);

            var doris = new Employee() { Name = "Doris", Title = "Build Engineer" };

            var ethan = new Manager() { Name = "Ethan", Title = "Release Manager" };
            doris.Manager = ethan;
            ethan.Employees.Add(doris);

            var frank = new Manager() { Name = "Frank", Title = "Director" };
            chloe.Manager = frank;
            ethan.Manager = frank;
            frank.Employees.Add(chloe);
            frank.Employees.Add(ethan);


            var employeeArray = new IEmployee[] { alice, bruce, chloe, doris, ethan, frank };
            for(int i = 0; i < employeeArray.Length; i++)
            {
                Console.WriteLine(employeeArray[i]); // employeeArray[i].ToString();
            }
            Console.WriteLine();
            Console.WriteLine();

            var employeeList = new List<IEmployee>() { alice, bruce, chloe, doris, ethan, frank };
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
