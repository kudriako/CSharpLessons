using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace CSharpLessons.OrganizationData
{
    public class OrganizationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public IQueryable<OrganizationModel.Employee>  ModelEmployees => Employees.Select(o => new OrganizationModel.Employee() { Name = o.Name });

        public OrganizationModel.Organization BuildOrganization(string name)
        {
            var org = new OrganizationModel.Organization(name);
            var entities = Employees.ToDictionary(e => e.Id);
            var employees = entities.ToDictionary(d => d.Key, d => {
                if (entities.Any(x => x.Value.ManagerId == d.Key))
                {
                    return (OrganizationModel.IEmployee)new OrganizationModel.Manager() { Name = d.Value.Name };
                }
                else
                {
                    return (OrganizationModel.IEmployee)new OrganizationModel.Employee() { Name = d.Value.Name };
                }
            });
            
            var directorKey = entities.First(x => x.Value.ManagerId == null).Key;
            org.Director = employees[directorKey];

            foreach(var employee in employees)
            {
                var managerId = entities[employee.Key].ManagerId ?? -1;
                employees.TryGetValue(managerId, out var manager);
                org.AddEmployee(employee.Value, manager as OrganizationModel.Manager);
            }

            return org;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CSharpLessons;Integrated Security=True");
        }
    }
}