using System;
using System.Collections.Generic;
using System.Reflection;
using WcfCalculatorClient.CalculatorServiceReference;

namespace WcfCalculatorClient
{
    class TestClient : IDisposable
    {
        private readonly CalculatorClient wcfClient;
        private readonly Dictionary<string, Delegate> operations;

        public TestClient()
        {
            wcfClient = new CalculatorClient();
            operations = new Dictionary<string, Delegate>()
            {
                { "^", new Func<double, double>(wcfClient.Sqrt)},
                {"+", new Func<double, double, double>(wcfClient.Add) },
                {"-", new Func<double, double, double>(wcfClient.Substract) },
                {"*", new Func<double, double, double>(wcfClient.Multiply) },
                {"/", new Func<double, double, double>(wcfClient.Divide) }
            };
        }

        public double PerformCalculation(string operation, double[] args)
        {
            if (!operations.ContainsKey(operation))
            {
                throw new ArgumentOutOfRangeException(nameof(operation));
            }

            try
            {
                if (args.Length == 1)
                {
                    return PerformCalculation(operation, args[0]);
                }

                return PerformCalculation(operation, args[0], args[1]);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        private double PerformCalculation(string operation, double arg)
        {
            return (double) operations[operation].DynamicInvoke(arg);
        }

        private double PerformCalculation(string operation, double arg, double arg1)
        {
            return (double)operations[operation].DynamicInvoke(arg, arg1);
        }

        public int GetArgumentNumber(string operation)
        {
            if (operations.ContainsKey(operation))
            {
                return operations[operation].GetMethodInfo().GetParameters().Length;
            }
            throw new ArgumentOutOfRangeException(nameof(operation));
        }

        public void Dispose()
        {
            wcfClient.Close();
        }

    }
}
