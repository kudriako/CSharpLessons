using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpLessons.OrganizationModel
{
    public class Manager : Employee
    {
        public Manager()
        {
            Employees = new List<IEmployee>();
        }

        public List<IEmployee> Employees { get; }

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
