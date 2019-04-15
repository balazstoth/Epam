using System;

namespace RationalNumbers
{
    struct Rational : IComparable<Rational>, IEquatable<Rational>
    {
        private int _numerator;
        private int _denominator;
        private const string InvalidDenominatorMsg = "Denominator cannot be 0!";

        public double Value { get { return _numerator / _denominator; } }

        public Rational(int num, int den)
        {
            if (den == 0)
                throw new ArgumentException(InvalidDenominatorMsg);

            if(den < 0) //Negative value
                _numerator = num * (-1);
            else
                _numerator = num;

            _denominator = Math.Abs(den);

            int greatestCommonDivisor = GCD(Math.Abs(_numerator), Math.Abs(_denominator));
            _numerator /= greatestCommonDivisor;
            _denominator /= greatestCommonDivisor;
        }

        public Rational(int number)
        {
            _numerator = number;
            _denominator = 1;
        }

        public int CompareTo(Rational other)
        {
            return this.Value.CompareTo(other);
        }

        public bool Equals(Rational other)
        {
            return this.Value.Equals(other.Value);
        }

        public override string ToString()
        {
            if (_denominator == 1)
                return _numerator.ToString();
            else
                return $"{_numerator}r{_denominator}";
        }

        private int GCD(int x, int y) => y == 0 ? x : GCD(y, x % y);

        public static Rational operator +(Rational first, Rational second)
        {
            GetCommonDec(ref first, ref second);
            return new Rational(first._numerator + second._numerator, first._denominator);
        }

        public static Rational operator -(Rational first, Rational second)
        {
            GetCommonDec(ref first, ref second);
            return new Rational(first._numerator - second._numerator, first._denominator);
        }

        public static Rational operator *(Rational first, Rational second)
        {
            return new Rational(first._numerator * second._numerator, first._denominator * second._denominator);
        }

        public static Rational operator /(Rational first, Rational second)
        {
            second = !second;
            return first * second;
        }

        public static Rational operator !(Rational first)
        {
            return new Rational(first._denominator, first._numerator);
        }

        private static void GetCommonDec(ref Rational f, ref Rational s)
        {
            if (f._denominator != s._denominator)
            {
                int newD = f._denominator* s._denominator;
                f._numerator *= s._denominator;
                s._numerator *= f._denominator;
                f._denominator = newD;
                s._denominator = newD;
            }
        }
    }
}
