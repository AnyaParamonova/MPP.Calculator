using System;
using System.ServiceModel;

namespace WcfCalculatorService
{
    public class Calculator : ICalculator
    {
        public double Add(double a, double b)
        {
            double result = a + b;
            CheckResult(nameof(Add), result);

            return result;
        }

        public double Substract(double a, double b)
        {
            double result = a - b;
            CheckResult(nameof(Add), result);

            return result;
        }

        public double Multiply(double a, double b)
        {
            double result =  a * b;
            CheckResult(nameof(Add), result);

            return result;
        }

        public double Divide(double a, double b)
        {
            if (b == 0d)
            {
                CalculationFault faut = new CalculationFault("Failed Divide : second parameter is zero.");
                throw new FaultException<CalculationFault>(faut);
            }
               

            double result = a/b;
            CheckResult(nameof(Add), result);
        
            return result;
        }

        public double Sqrt(double a)
        {
            if (a < 0)
            {
                CalculationFault faut = new CalculationFault("Failed Sqrt : parameter is less than zero.");
                throw new FaultException<CalculationFault>(faut);
            }

            double result =  Math.Sqrt(a);
            CheckResult(nameof(Add), result);

            return result;
        }

        private void CheckResult(string operation, double result)
        {
            if (double.IsNaN(result))
            {
                CalculationFault faut = new CalculationFault("Failed " + operation + " : result is NaN.");
                throw new FaultException<CalculationFault>(faut);
            }
            if (double.IsInfinity(result))
            {
                CalculationFault faut = new CalculationFault("Failed " + operation + " : result is Infinite.");
                throw new FaultException<CalculationFault>(faut);
            }
        }
    }

}
