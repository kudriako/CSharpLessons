namespace CSharpLessons.Fractions
{
    public class Fraction
    {
        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public int Numerator { get; }

        public int Denominator { get; }

        public override string ToString()
        {
            return $"{Numerator} / {Denominator}";
        }

        //private static int GetGdc(int a, int b)
        //{
        //    if (b == 0)
        //        return a > 0 ? a : -a;
        //   return GetGdc(b, a % b);
        //}
    }
}
