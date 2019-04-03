using System;
using System.Collections.Generic;
using System.Linq;
using CSharpLessons.OrganizationModel;
using CSharpLessons.OrganizationModel.Offices;

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
            var doris = new Employee() { Name = "Doris", Title = "Build Engineer" };
            var ethan = new Manager() { Name = "Ethan", Title = "Release Manager" };
            var frank = new Manager() { Name = "Frank", Title = "Director" };

            var organization = new Organization("DoodleSoft");

            var taxOffice = new TaxOffice();
            var insuranceOffice = new InsuranceOffice();
            var pensionFundOffice = new PensionFundOffice();

            organization.AddEmployee(alice, chloe);
            organization.AddEmployee(bruce, chloe);
            organization.AddEmployee(chloe, frank);
            organization.AddEmployee(doris, ethan);
            organization.AddEmployee(ethan, frank);
            organization.AddEmployee(frank, null);
            organization.Director = frank;


            // 1. Lamda syntax

            var count = organization.Employees.Where(e => e.Manager == chloe).Count();
            Console.WriteLine(count);

            // 2. Query syntax

            var count2 = (from employee in organization.Employees 
                         where employee.Manager == chloe
                         select employee).Count();
            Console.WriteLine(count2);

            Console.WriteLine();

            // 3. See organization.Employees for IEnumerable implementation

            // 4. Deffered execution example
            var words = new string[] {"aaaaaaaaaaaaaaaaaaaaa", "bbbbbbbbbbb", "ccc"};
            var result = words.Where(w => w.Length < 5);
            words[0] = "aaa";
            Console.WriteLine($"Number of short words: {result.Count()}"); // Displays 2 becaus result is evaluated at use after word[0] is changed.

            // 5. Multiple evaluation example
            var source = new string[] {"aaaaaaaaaaaaaaaaaaaaa", "bbbbbbbbbbb", "ccc"};
            var enumeration = source.Where(s => s.Length < 5);
            // Here we check that enumeration contains at least one element and we can use it with no errors.
            if (enumeration.Any())
            {
                // this change can be made from another place by another thread.
                // even more this can be done in any source: in database neither in file neither in XML document whatever.
                source[2] = "asdasdasdasdasdasdasdasd";
                // this will throw an exception then:
                // An unhandled exception of type 'System.InvalidOperationException' occurred in System.Linq.dll: 
                // 'Sequence contains no elements'.
                Console.WriteLine(enumeration.First());
            }
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
