using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomanNumbers;

namespace RomanNumbersTest
{
    [TestClass]
    public class ExtendIntTest
    {
        public static IEnumerable<object[]> GetIncorrectData()
        {
            yield return new object[] { -1 };
            yield return new object[] { 0 };
            yield return new object[] { 4000 };
            yield return new object[] { 5000 };
        }
        public static IEnumerable<object[]> GetCorrectData()
        {
            yield return new object[] { 1, "I" };
            yield return new object[] { 9, "IX" };
            yield return new object[] { 19, "XIX" };
            yield return new object[] { 50, "L" };
            yield return new object[] { 100, "C" };
            yield return new object[] { 300, "CCC" };
            yield return new object[] { 478, "CDLXXVIII" };
            yield return new object[] { 501, "DI" };
            yield return new object[] { 990, "CMXC" };
            yield return new object[] { 1000, "M" };
            yield return new object[] { 3999, "MMMCMXCIX" };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCorrectData), DynamicDataSourceType.Method)]
        public void Test_ToRoman_ValidValue(int number, string value)
        {
            var result = number.ToRoman();
            Assert.AreEqual(value, result);
        }

        [DataTestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DynamicData(nameof(GetIncorrectData),DynamicDataSourceType.Method)]
        public void Test_ToRoman_InvalidValue(int number)
        {
            var result = number.ToRoman();
        }
    }
}
