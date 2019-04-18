using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RationalNumbers;

namespace RationalNumberTest
{
    [TestClass]
    public class RationalNumberTest
    {
        public static IEnumerable<object[]> GetComparableValues()
        {
            Rational r1 = new Rational(1, 2);
            Rational r2 = new Rational(3, 4);
            Rational r3 = new Rational(-1, 2);
            Rational r4 = new Rational(-3, 4);
            yield return new object[] { r1, r2, -1 };
            yield return new object[] { r2, r1, 1 };
            yield return new object[] { r1, r1, 0 };
            yield return new object[] { r3, r4, 1 };
            yield return new object[] { r4, r3, -1 };
            yield return new object[] { r3, r3, 0 };
            yield return new object[] { r1, r3, 1 };
            yield return new object[] { r3, r1, -1 };
        }
        public static IEnumerable<object[]> GetEqualValues()
        {
            Rational r1 = new Rational(4, 5);
            Rational r2 = new Rational(5, 2);
            Rational r3 = new Rational(-3, 4);
            Rational r4 = new Rational(5, -6);
            yield return new object[] { r1, r2, false };
            yield return new object[] { r1, r1, true };
            yield return new object[] { r2, r1, false };
            yield return new object[] { r3, r4, false };
            yield return new object[] { r4, r3, false };
            yield return new object[] { r4, r4, true};
        }
        public static IEnumerable<object[]> GetToStringValues()
        {
            Rational r1 = new Rational(4, 5);
            Rational r2 = new Rational(5, 1);
            yield return new object[] { r1, "4/5"};
            yield return new object[] { r2, "5"};
        }
        public static IEnumerable<object[]> GetAddValues()
        {
            yield return new object[] { new Rational(4, 1), new Rational(6, 3), new Rational(18,3) };
            yield return new object[] { new Rational(-4, 1), new Rational(-6, 3), new Rational(-18,3) };
            yield return new object[] { new Rational(-4, 1), new Rational(6, 3), new Rational(-6, 3) };
        }
        public static IEnumerable<object[]> GetSubtractValues()
        {
            yield return new object[] { new Rational(4, 1), new Rational(6, 3), new Rational(6, 3) };
            yield return new object[] { new Rational(-4, 1), new Rational(-6, 3), new Rational(-6, 3) };
            yield return new object[] { new Rational(-4, 1), new Rational(6, 3), new Rational(-18, 3) };
        }
        public static IEnumerable<object[]> GetNegativeValues()
        {
            yield return new object[] { new Rational(4, 7), new Rational(-4, 7) };
            yield return new object[] { new Rational(-9,4), new Rational(9, 4) };
            yield return new object[] { new Rational(9, -4), new Rational(9, 4) };
        }
        public static IEnumerable<object[]> GetMultiplicationValues()
        {
            yield return new object[] { new Rational(0, 1), new Rational(5, 3), new Rational(0, 3) };
            yield return new object[] { new Rational(4, 9), new Rational(2, 3), new Rational(8, 27) };
            yield return new object[] { new Rational(-1, 3), new Rational(4, 3), new Rational(-4, 9) };
            yield return new object[] { new Rational(-4, 1), new Rational(-6, 7), new Rational(24, 7) };
        }
        public static IEnumerable<object[]> GetInverseValues()
        {
            yield return new object[] { new Rational(-9, 4), new Rational(4, -9) };
            yield return new object[] { new Rational(9, 4), new Rational(4, 9) };
        }
        public static IEnumerable<object[]> GetDivideValues()
        {
            yield return new object[] { new Rational(0, 1), new Rational(2, 11), new Rational(0, 2) };
            yield return new object[] { new Rational(6, 9), new Rational(4, 2), new Rational(12, 36) };
            yield return new object[] { new Rational(-1, 3), new Rational(4, 3), new Rational(-3, 12) };
            yield return new object[] { new Rational(-4, 6), new Rational(-6, 7), new Rational(28, 36) };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_CreateRational()
        {
            Rational r1 = new Rational(3, 0);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetComparableValues), DynamicDataSourceType.Method)]
        public void Test_CompareTo(Rational first, Rational second, int expected)
        {
            int result = first.CompareTo(second);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetEqualValues), DynamicDataSourceType.Method)]
        public void Test_Equals(Rational first, Rational second, bool expected)
        {
            bool result = first.Equals(second);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetToStringValues), DynamicDataSourceType.Method)]
        public void Test_CompareTo(Rational first, string expected)
        {
            string result = first.ToString();
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetAddValues), DynamicDataSourceType.Method)]
        public void Test_Add(Rational first, Rational second, Rational expected)
        {
            Rational result = first + second;
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetSubtractValues), DynamicDataSourceType.Method)]
        public void Test_Subtract(Rational first, Rational second, Rational expected)
        {
            Rational result = first - second;
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetNegativeValues), DynamicDataSourceType.Method)]
        public void Test_Negative(Rational first, Rational expected)
        {
            Rational result = -first;
            Assert.AreEqual(result, expected);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetMultiplicationValues), DynamicDataSourceType.Method)]
        public void Test_Multiplication(Rational first, Rational second, Rational expected)
        {
            Rational result = first * second;
            Assert.AreEqual(result, expected);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetInverseValues), DynamicDataSourceType.Method)]
        public void Test_Inverse(Rational first, Rational expected)
        {
            Rational result = !first;
            Assert.AreEqual(result, expected);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetDivideValues), DynamicDataSourceType.Method)]
        public void Test_Divide(Rational first, Rational second, Rational expected)
        {
            Rational result = first / second;
            Assert.AreEqual(expected, result);
        }

    }
}
