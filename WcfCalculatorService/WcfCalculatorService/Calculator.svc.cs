using System;

namespace WcfCalculatorService
{
    public class Calculator : ICalculator
    {
        public double Add(double a, double b)
        {
            return a + b;
        }

        public double Substract(double a, double b)
        {
            return a - b;
        }

        public double Multiply(double a, double b)
        {
            return a * b;
        }

        public double Divide(double a, double b)
        {
            return a/b;
        }

        public double Sqrt(double a)
        {
            return Math.Sqrt(a);
        }
    }
}
