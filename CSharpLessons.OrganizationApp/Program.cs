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

            Console.WriteLine("Organization:");
            Console.WriteLine(organization);
            Console.WriteLine();
            Console.WriteLine();

            // Creating context
            CSharpLessons.OrganizationData.OrganizationContext context = new CSharpLessons.OrganizationData.OrganizationContext();

            Console.WriteLine("From database");
            Console.WriteLine("ID, NAME, TITLE, MANAGERID, ISMANAGER");
            foreach(var emp in context.Employees)
            {
                Console.WriteLine($"{emp.Id}, {emp.Name}, {emp.Title}, {emp.ManagerId}, {emp.IsManager}");
            }
            Console.WriteLine();
            Console.WriteLine();


            var org = context.BuildOrganization("New organization");
            Console.WriteLine("Organization was restored from database:");
            Console.WriteLine(org);

            Console.WriteLine("Now adding Ivor as Sales Manager.");
            Console.WriteLine(org);
            var ivor = new Manager() { Name = "Ivor", Title = "Sales Manager" };
            org.AddEmployee(ivor, org.Director);
            
            Console.WriteLine("Saving to database.");
            context.AddEmployee(ivor);
            Console.WriteLine();
            Console.WriteLine();

            // Displaying database content once more
            Console.WriteLine("From database");
            Console.WriteLine("ID, NAME, TITLE, MANAGERID, ISMANAGER");
            foreach(var emp in context.Employees)
            {
                Console.WriteLine($"{emp.Id}, {emp.Name}, {emp.Title}, {emp.ManagerId}, {emp.IsManager}");
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
