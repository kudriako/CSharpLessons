using System;

namespace CSharpLessons.OrganizationModel
{
    public interface IEmployee
    {
        string Name { get; set; }

        string Title { get; set; }

        Manager Manager { get; set; }
    }
}
