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

            foreach(var employee in organization.Employees)
            {
                Console.WriteLine(employee);
            }

            CSharpLessons.OrganizationData.OrganizationContext context = new CSharpLessons.OrganizationData.OrganizationContext();
            
            foreach(var emp in context.Employees)
            {
                Console.WriteLine($"{emp.Id}, {emp.Name}, {emp.ManagerId}");
            }
            var org = context.BuildOrganization("New organization");
            
            context.AddEmployee(doris);

            var pi = CalculatePi(1000);
            Console.WriteLine(pi);
            Console.WriteLine(CalculatePi(100000));
            Console.WriteLine(CalculatePi(10000000));
            Console.WriteLine(Math.PI);
            End();
        }

        private static double CalculatePi(int n)
        {
            var randomX = new Random(56675765);
            var randomY = new Random(98798768);
            var count = Enumerable.Repeat(0, n).Where(x => IsInCircle(randomX.NextDouble(), randomY.NextDouble())).Count();
            return 4.0 * count / n;
        }
        private static bool IsInCircle(double x, double y)
        {
            return x * x + y * y < 1.0;
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
