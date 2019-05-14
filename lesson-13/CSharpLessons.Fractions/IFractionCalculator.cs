using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLessons.Fractions
{
    public interface IFractionCalculator
    {
        Fraction Add(Fraction a, Fraction b);

        Fraction Subtract(Fraction a, Fraction b);

        Fraction Multiply(Fraction a, Fraction b);

        Fraction Divide(Fraction a, Fraction b);
    }
}
