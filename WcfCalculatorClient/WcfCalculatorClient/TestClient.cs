using System;
using System.Collections.Generic;
using System.Linq;
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
                return PerformCalculation(operation, args.Cast<object>().ToArray());
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        private double PerformCalculation(string operation, params object[] args)
        {
            return (double) operations[operation].DynamicInvoke(args);
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
