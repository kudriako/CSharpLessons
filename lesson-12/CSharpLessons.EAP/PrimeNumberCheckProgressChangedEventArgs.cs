using System.ComponentModel;

namespace CSharpLessons.EAP
{
    public class PrimeNumberCheckProgressChangedEventArgs : ProgressChangedEventArgs
    {
        public PrimeNumberCheckProgressChangedEventArgs(decimal number, double seconds, int progressPercentage, object userSuppliedState) 
            : base(progressPercentage, userSuppliedState)
        {
            Number = number;
            Seconds = seconds;
        }

        public decimal Number { get; private set; }

        public double Seconds { get; private set; }
    }
}