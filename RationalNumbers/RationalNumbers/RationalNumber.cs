using System;

namespace RationalNumbers
{
    public struct Rational : IComparable<Rational>, IEquatable<Rational>
    {
        private int _numerator;
        private int _denominator;

        public double Value { get { return (double)_numerator / (double)_denominator; } }

        public Rational(int num, int den)
        {
            if (den == 0)
                throw new ArgumentException(den.ToString());

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

        private int GCD(int x, int y) => y == 0 ? x : GCD(y, x % y);
        private static void GetCommonDec(ref Rational f, ref Rational s)
        {
            if (f._denominator != s._denominator)
            {
                int newD = f._denominator * s._denominator;
                f._numerator *= s._denominator;
                s._numerator *= f._denominator;
                f._denominator = newD;
                s._denominator = newD;
            }
        }
        public int CompareTo(Rational other)
        {
            return this.Value.CompareTo(other.Value);
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
        public static Rational operator -(Rational first)
        {
            Rational r = new Rational(-first._numerator, first._denominator);
            return r;
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
    }
}
