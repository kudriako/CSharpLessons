using System.Diagnostics;

namespace CSharpLessons.TAP
{
    public class PrimeNumberResult
    {
        public PrimeNumberResult(decimal number, bool isPrime, Stopwatch stopwatch)
        {
            Number = number;
            IsPrime = isPrime;
            Seconds = stopwatch.Elapsed.TotalSeconds;
        }

        public decimal Number { get; private set; }

        public bool IsPrime { get; private set; }

        public double Seconds { get; private set; }
    }
}