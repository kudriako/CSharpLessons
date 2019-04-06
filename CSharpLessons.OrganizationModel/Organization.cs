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

        private event EventHandler<IEmployee> _employeeAdded;

        private int _subscriptionCount;

        public string Name { get; }

        public IEnumerable<IEmployee> Employees => EnumerateEmployees();

        public Manager Director { get; private set; }

        public event EventHandler<IEmployee> EmployeeAdded
        {
            add
            {
                _employeeAdded += value;
                Console.WriteLine("Someone subscribed to event.");
                _subscriptionCount++;
                Console.WriteLine($"Number of subscribers is {_subscriptionCount}");
                
            }
            remove
            {
                _employeeAdded -= value;
                Console.WriteLine("Someone unsubscribed from event.");
                _subscriptionCount--;
                Console.WriteLine($"Number of subscribers is {_subscriptionCount}");
            }
        }

        public Organization(string name)
        {
            Name = name;
        }
        
        public void AddEmployee(IEmployee employee, Manager manager)
        {
            _employees.Add(employee);
            if (manager == null && Director != null)
            {
                throw new InvalidOperationException("Can be only one Director.");
            }
            else if (manager == null)
            {
                Director = employee as Manager;
            }
            else
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
            _employeeAdded?.Invoke(this, employee);
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

        private IEnumerable<IEmployee> EnumerateEmployees()
        {
            return EnumerateEmployees(Director);
        }

        private IEnumerable<IEmployee> EnumerateEmployees(IEmployee employee)
        {
            if (employee == null)
                yield break;
            yield return employee;
            foreach(var e in employee.Employees.SelectMany(EnumerateEmployees))
            {
                yield return e;
            }
        }
    }
}