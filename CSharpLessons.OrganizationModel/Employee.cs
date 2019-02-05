using System;
using System.Text;

namespace CSharpLessons.OrganizationModel
{
    public class Employee : IEmployee
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public Manager Manager { get; set; }

        public string EmployeeCard
        { 
            get
            {
                string delimiter = new String('-', 32);
                var sb = new StringBuilder();
                sb.AppendLine(delimiter);
                sb.AppendLine(Name);
                sb.AppendLine(delimiter);
                sb.Append("Title: ");
                sb.AppendLine(Title);
                sb.Append("Manager: ");
                sb.Append(Manager);
                sb.AppendLine();
                sb.AppendLine(delimiter);
                return sb.ToString();
            } 
        }

        public override string ToString()
        {
            return $"{Name}, {Title}";
        }
    }
}
