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
            CheckResult(nameof(Substract), result);

            return result;
        }

        public double Multiply(double a, double b)
        {
            double result =  a * b;
            CheckResult(nameof(Multiply), result);

            return result;
        }

        public double Divide(double a, double b)
        {
            double result = a/b;
            CheckResult(nameof(Divide), result);
        
            return result;
        }

        public double Sqrt(double a)
        {
            double result =  Math.Sqrt(a);
            CheckResult(nameof(Sqrt), result);

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
