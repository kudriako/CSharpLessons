using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpLessons.OrganizationModel
{
    public class Manager : EmployeeBase
    {
        private readonly List<IEmployee> _employees = new List<IEmployee>();

        public override IEnumerable<IEmployee> Employees => _employees;

        public void AddEmployee(IEmployee employee)
        {
            _employees.Add(employee);
            employee.Manager = this;
        }

        protected override void AppendEmployeeCardDetails(StringBuilder sb) 
        {
            base.AppendEmployeeCardDetails(sb);
            sb.AppendLine("Manager of: ");
            foreach(var employee in Employees)
            {
                sb.Append("    ");
                sb.AppendLine(employee.ToString());
            }
        }
    }
}
