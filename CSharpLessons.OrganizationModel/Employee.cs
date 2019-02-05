using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpLessons.OrganizationModel
{
    public class Employee : EmployeeBase
    {
        public override IEnumerable<IEmployee> Employees 
        {
            get 
            {
                // returns empty enumeration.
                return Enumerable.Empty<IEmployee>(); 
            }
        }
    }
}
