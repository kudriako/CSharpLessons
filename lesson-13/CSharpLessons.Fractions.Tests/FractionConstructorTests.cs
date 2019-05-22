using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLessons.Fractions.Tests
{
    // http://idms0/confluence/pages/viewpage.action?pageId=28901379
    [TestClass]
    public class FractionConstructorTests
    {
        [TestMethod]
        public void Fraction_Constructor_NumeratorAndDenominatorShouldBeSet()
        {
            // Arrange
            int numerator = 5;
            int denominator = 7;

            // Act
            var fraction = new Fraction(numerator, denominator);

            //Assert
            Assert.AreEqual(numerator, fraction.Numerator);
            Assert.AreEqual(denominator, fraction.Denominator);
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Fraction_Constructor_DenominatorCannotBeZero()
        //{
        //    // Arrange
        //    int numerator = 5;
        //    int denominator = 0;

        //    // Act
        //    new Fraction(numerator, denominator);

        //    //Assert
        //    Assert.Fail("Expected ArgumentException to be thrown.");
        //}

        //[TestMethod]
        //public void Fraction_Constructor_NumeratorAndDenominatorShouldBeDenominated()
        //{
        //    // Arrange
        //    int numerator = 15;
        //    int denominator = 3;

        //    // Act
        //    var fraction = new Fraction(numerator, denominator);

        //    //Assert
        //    Assert.AreEqual(5, fraction.Numerator);
        //    Assert.AreEqual(1, fraction.Denominator);
        //}
    }
}
