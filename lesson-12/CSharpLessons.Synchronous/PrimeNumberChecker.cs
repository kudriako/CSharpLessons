using System;

namespace CSharpLessons.Synchronous
{
    public class PrimeNumberChecker
    {
        public bool IsPrime(decimal number)
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
    }
}