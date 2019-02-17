using System;

namespace CSharpLessons.OrganizationModel.Offices
{
    public class InsuranceOffice
    {
        public void Subscribe(Organization organization)
        {
            organization.EmployeeAdded += HandleOrganizationEmployeeAdded;
        }

        public void Unsubscribe(Organization organization)
        {
            organization.EmployeeAdded -= HandleOrganizationEmployeeAdded;
        }

        private void HandleOrganizationEmployeeAdded(object sender, IEmployee employee)
        {
            var organization = sender as Organization;
            Insure(organization, employee);
        }

        private void Insure(Organization organization, IEmployee employee)
        {
            Console.WriteLine($"You are insured as {employee} of {organization.Name}");
        }
    }
}