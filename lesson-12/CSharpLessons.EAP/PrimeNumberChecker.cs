using System;
using System.Diagnostics;

namespace CSharpLessons.EAP
{
    public class PrimeNumberChecker
    {
        public bool IsPrime(decimal number, out double seconds)
        {
            if (number < 1)
            {
                throw new InvalidOperationException($"{number} is invalid number.");
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                if (number % 2 == 0)
                    return false;
                var limit = (uint)Math.Sqrt((double)number);
                for(uint i = 3; i <= limit; i += 2)
                {
                    if (number % i == 0)
                        return false;
                }
                return true;
            }
            finally
            {
                stopwatch.Stop();
                seconds = stopwatch.Elapsed.TotalSeconds;
            }
        }
    }
}