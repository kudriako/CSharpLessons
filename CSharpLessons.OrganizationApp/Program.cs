﻿using System;
using System.Collections.Generic;

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

            var organization = new Organization();

            var taxOffice = new TaxOffice();
            var insuranceOffice = new InsuranceOffice();
            var pensionFundOffice = new PensionFundOffice();

            organization.EmployeeAdded += taxOffice.RegisterEmployee;
            organization.EmployeeAdded += insuranceOffice.Insure;
            organization.EmployeeAdded += pensionFundOffice.AddToPensionProgram;

            organization.AddEmployee(alice, chloe);
            organization.AddEmployee(bruce, chloe);
            organization.AddEmployee(chloe, frank);
            organization.AddEmployee(doris, ethan);
            organization.AddEmployee(ethan, frank);
            organization.AddEmployee(frank, null);
            organization.Director = frank;

            organization.EmployeeAdded -= taxOffice.RegisterEmployee;
            organization.EmployeeAdded -= insuranceOffice.Insure;
            organization.EmployeeAdded -= pensionFundOffice.AddToPensionProgram;

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
