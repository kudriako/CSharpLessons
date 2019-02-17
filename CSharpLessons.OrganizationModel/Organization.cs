using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CSharpLessons.OrganizationModel.Offices;

namespace CSharpLessons.OrganizationModel
{
    public class Organization
    {
        private List<IEmployee> _employees = new List<IEmployee>();

        public string Name { get; }

        public IEnumerable<IEmployee> Employees => _employees;

        public IEmployee Director { get; set; }

        public event EventHandler<IEmployee> EmployeeAdded;

        public Organization(string name)
        {
            Name = name;
        }
        
        public void AddEmployee(IEmployee employee, Manager manager)
        {
            _employees.Add(employee);
            if (manager != null)
            {
                manager.AddEmployee(employee);
            }
            OnEmployeeAdded(employee);
        }

        public void FireEmployee(IEmployee employee)
        {
            if (employee.Manager != null)
            {
                employee.Manager.RemoveEmployee(employee);
            }
            _employees.Remove(employee);
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
            AppendEmployee(sb, Director, 0);
            return sb.ToString();
        }

        protected virtual void OnEmployeeAdded(IEmployee employee)
        {
            EmployeeAdded?.Invoke(this, employee);
        }

        private void AppendEmployee(StringBuilder sb, IEmployee employee, int level)
        {
            sb.Append(employee);
            sb.AppendLine();
            foreach(var childEmployee in employee.Employees)
            {
                sb.Append(new string('\t', level + 1));
                AppendEmployee(sb, childEmployee, level + 1);
            }
        }
    }
}