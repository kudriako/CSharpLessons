using System;

namespace CSharpLessons.OrganizationModel.Offices
{
    public class PensionFundOffice
    {
        public void AddToPensionProgram(object sender, IEmployee employee)
        {
            Console.WriteLine($"Wait for retirement pension, {employee}!!!");
        }
    }
}