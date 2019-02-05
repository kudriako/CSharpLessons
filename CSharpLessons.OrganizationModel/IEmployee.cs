using System;
using System.Collections.Generic;

namespace CSharpLessons.OrganizationModel
{
    public interface IEmployee
    {
        string Name { get; set; }

        string Title { get; set; }

        Manager Manager { get; set; }

        IEnumerable<IEmployee> Employees { get; }

        string EmployeeCard { get;}
    }
}
