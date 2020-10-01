using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Calculator.Test.Unit
{
    [TestFixture]
    public class CalculatorUnitTest
    {
        private Calculator uut;
        private double _result;

        [SetUp]
        public void Setup()
        {
            uut = new Calculator();
            _result = 0;
        }

        [TestCase(6,13,19)]
        [TestCase(-5,5,0)]
        [TestCase(5.4, 13.8,19.2)]
        [TestCase(7, 0, 7)]
        [TestCase(3, 1.7, 4.7)]
        public void Add_TwoNumbers_ReturnCorrectResult(double firstNumber, double secondNumber, double result)
        {
            _result = uut.Add(firstNumber, secondNumber);

            Assert.That(_result, Is.EqualTo(result).Within(0.005));

            // alternate way:
            Assert.AreEqual(_result, result, 0.005);
        }

        [TestCase(5, 13, -8)]
        [TestCase(10, 8, 2)]
        [TestCase(-5, 5, -10)]
        [TestCase(5.4, 13.8, -8.4)]
        [TestCase(7, 0, 7)]
        public void Subtract_TwoNumbers_ReturnCorrectResult(double firstNumber, double secondNumber, double result)
        {
            _result = uut.Subtract(firstNumber, secondNumber);

            Assert.That(_result, Is.EqualTo(result).Within(0.005));
        }

        [TestCase(5, 13, 65)]
        [TestCase(-5, 5, -25)]
        [TestCase(5.4, 13.8, 74.52)]
        [TestCase(8, 0, 0)]
        public void Multiply_TwoNumbers_ReturnCorrectResult(double firstNumber, double secondNumber, double result)
        {
            _result = uut.Multiply(firstNumber, secondNumber);

            Assert.That(_result, Is.EqualTo(result).Within(0.005));
        }

        [TestCase(5, 3, 125)]
        [TestCase(-5, 5, -3125)]
        [TestCase(5.4, 3.8, 606.8709)]
        [TestCase(7, 0, 1)]
        [TestCase(0, 7, 0)]
        public void Power_TwoNumbers_ReturnCorrectResult(double firstNumber, double secondNumber, double result)
        {
            _result = uut.Power(firstNumber, secondNumber);

            Assert.That(_result, Is.EqualTo(result).Within(0.005));
        }

        [TestCase(10, 5, 2)]
        [TestCase(-10, 5, -2)]
        [TestCase(-10, -2, 5)]
        public void Divide_TwoNumbers_ReturnCorrectResult(double firstNumber, double secondNumber, double result)
        {
            _result = uut.Divide(firstNumber, secondNumber);

            Assert.That(_result, Is.EqualTo(result).Within(0.005));
        }

        [TestCase(1, 0)]
        [TestCase(10,0)]
        [TestCase(-19,0)]
        public void Divide_TwoNumbers_ThrowException(double firstNumber, double secondNumber)
        {
            try
            {
                _result = uut.Divide(firstNumber, secondNumber);
                Assert.Fail(); // if this line is reached, it means no exception was thrown = fail
            }
            catch (DivideByZeroException){}
        }

        [TestCase(10, 5, 2, 1)]
        [TestCase(-10, 5, -2, 1)]
        [TestCase(-10, -2, -2.5, -2)]
        [TestCase(1, 2, 2, 0.25)]
        public void Divide_OneNumberAndAccumulator_ReturnCorrectResult(double firstNumber, double secondNumber, double thirdNumber, double result)
        {
            uut.Divide(firstNumber, secondNumber);
            _result = uut.Divide(thirdNumber);

            Assert.That(_result, Is.EqualTo(result).Within(0.005));
        }

        [TestCase()]
        public void Divide_OneNumberAndAccumulator_ThrowException()
        {
            try
            {
                uut.Add(10, 10);
                _result = uut.Divide(0);
                Assert.Fail();
            }
            catch (DivideByZeroException){}
        }

        [TestCase(10, 5, 2, 17)]
        [TestCase(-10, 5, -2, -7)]
        [TestCase(-10, -2, -2.5, -14.5)]
        [TestCase(1, 2, 2, 5)]
        public void Add_OneNumberAndAccumulator_ReturnCorrectResult(double firstNumber, double secondNumber, double thirdNumber, double result)
        {
            uut.Add(firstNumber, secondNumber);
            _result = uut.Add(thirdNumber);

            Assert.That(_result, Is.EqualTo(result).Within(0.005));
        }

        [TestCase(10, 5, 2, 3)]
        [TestCase(-10, 5, -2, -13)]
        [TestCase(-10, -2, -2.5, -5.5)]
        [TestCase(1, 2, 2, -3)]
        public void Subtract_OneNumberAndAccumulator_ReturnCorrectResult(double firstNumber, double secondNumber, double thirdNumber, double result)
        {
            uut.Subtract(firstNumber, secondNumber);
            _result = uut.Subtract(thirdNumber);

            Assert.That(_result, Is.EqualTo(result).Within(0.005));
        }

        [TestCase(10, 5, 2, 100)]
        [TestCase(-10, 5, -2, 100)]
        [TestCase(-10, -2, -2.5, -50)]
        [TestCase(1, 2, 2, 4)]
        public void Multiply_OneNumberAndAccumulator_ReturnCorrectResult(double firstNumber, double secondNumber, double thirdNumber, double result)
        {
            uut.Multiply(firstNumber, secondNumber);
            _result = uut.Multiply(thirdNumber);

            Assert.That(_result, Is.EqualTo(result).Within(0.005));
        }

        [TestCase(10, 5, 2, 10000000000)]
        [TestCase(-10, 5, -2, 0.0000000001)]
        [TestCase(-10, -2, -2.5, 100000)]
        [TestCase(1, 2, 2, 1)]
        public void Power_OneNumberAndAccumulator_ReturnCorrectResult(double firstNumber, double secondNumber, double thirdNumber, double result)
        {
            uut.Power(firstNumber, secondNumber);
            _result = uut.Power(thirdNumber);

            Assert.That(_result, Is.EqualTo(result).Within(0.005));
        }

        [TestCase()]
        public void Clear_ClearsAccumulator_AccumulatorIsCleared()
        {
            uut.Add(2, 3);
            _result = uut.Add(5);
            Assert.That(_result, Is.EqualTo(10));

            uut.Clear();

            _result = uut.Add(5);

            Assert.That(_result, Is.EqualTo(5));
        }
    }
}
