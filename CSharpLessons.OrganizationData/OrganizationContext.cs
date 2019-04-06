using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Model = CSharpLessons.OrganizationModel;

namespace CSharpLessons.OrganizationData
{
    public class OrganizationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CSharpLessons;Integrated Security=True");
        }

        public Model.Organization BuildOrganization(string name)
        {
            var org = new Model.Organization(name);

            var list = Employees
                // Create a pair of employee and id of its manager.
                .Select(entity => 
                    new 
                    { 
                        Employee = entity.GetModel(), 
                        ManagerId = entity.ManagerId
                    }
                )
                // Materialize (gets result of) a query.
                .ToList();

            foreach(var item in list)
            {
                var manager = list.SingleOrDefault(x => x.Employee.Id == (item.ManagerId ?? -1))?.Employee as Model.Manager;
                org.AddEmployee(item.Employee, manager);
            }

            return org;
        }

        public void AddEmployee(Model.IEmployee employee)
        {
            var entity = new Employee(employee);
            Employees.Add(entity);
            SaveChanges();
        }
    }
}