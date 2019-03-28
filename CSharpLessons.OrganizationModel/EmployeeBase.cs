using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace CSharpLessons.OrganizationModel
{
    [Serializable]
    public abstract class EmployeeBase : IEmployee
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public Manager Manager { get; set; }

        //[JsonIgnore]
        //[XmlIgnore]
        public abstract IEnumerable<IEmployee> Employees { get; }

        [JsonIgnore]
        public string EmployeeCard
        { 
            get
            {
                string delimiter = new String('-', 32);
                var sb = new StringBuilder();
                sb.AppendLine(delimiter);
                sb.AppendLine(Name);
                sb.AppendLine(delimiter);
                AppendEmployeeCardDetails(sb);
                sb.AppendLine(delimiter);
                return sb.ToString();
            } 
        }

        protected virtual void AppendEmployeeCardDetails(StringBuilder sb) 
        {
            sb.Append("Title: ");
            sb.AppendLine(Title);
            sb.Append("Manager: ");
            sb.Append(Manager);
            sb.AppendLine();
        }

        public override string ToString()
        {
            return $"{Name}, {Title}";
        }
    }
}
