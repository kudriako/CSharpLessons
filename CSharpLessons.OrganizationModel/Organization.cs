using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpLessons.OrganizationModel
{
    public class Organization
    {
        private List<IEmployee> _employees = new List<IEmployee>();

        public IEnumerable<IEmployee> Employees => _employees;

        public void AddEmployee(IEmployee employee, Manager manager)
        {
            _employees.Add(employee);
            if (manager != null)
            {
                manager.AddEmployee(employee);
            }
        }

        public void PrintEmployeeCards()
        {
            foreach(var employee in _employees)
            {
                Console.WriteLine(employee.EmployeeCard);
                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach(var employee in _employees)
            {
                sb.Append(employee);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}