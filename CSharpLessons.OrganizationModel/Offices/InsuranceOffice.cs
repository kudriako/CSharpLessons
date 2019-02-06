using System;

namespace CSharpLessons.OrganizationModel.Offices
{
    public class InsuranceOffice
    {
        public void Insure(object sender, IEmployee employee)
        {
            Console.WriteLine($"You are insured, {employee}!!!");
        }
    }
}