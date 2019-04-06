using System;
using Model = CSharpLessons.OrganizationModel;

namespace CSharpLessons.OrganizationData
{
    public class Employee
    {
        public Employee()
        {
        }

        public Employee(Model.IEmployee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
            Id = employee.Id;
            Name = employee.Name;
            Title = employee.Title;
            ManagerId = employee.Manager?.Id;
            IsManager = employee is Model.Manager;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public int? ManagerId { get; set; }

        public bool IsManager { get; set; }

        public Model.IEmployee GetModel()
        {
            Model.IEmployee model = IsManager
                ? new Model.Manager() as Model.IEmployee 
                : new Model.Employee() as Model.IEmployee;
            model.Id = Id;
            model.Name = Name;
            model.Title = Title;
            return model;
        }
    }
}
