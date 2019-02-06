using System;

namespace CSharpLessons.OrganizationModel
{
    public class TaxOffice
    {
        public void RegisterEmployee(IEmployee employee)
        {
            Console.WriteLine($"Now pay taxes, {employee}!!!");
        }
    }
}
