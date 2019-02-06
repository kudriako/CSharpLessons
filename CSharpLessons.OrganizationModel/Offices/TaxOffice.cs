using System;

namespace CSharpLessons.OrganizationModel.Offices
{
    public class TaxOffice
    {
        public void RegisterEmployee(object sender, IEmployee employee)
        {
            Console.WriteLine($"Now pay taxes, {employee}!!!");
        }
    }
}
