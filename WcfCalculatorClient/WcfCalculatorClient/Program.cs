using System;
using System.Collections.Generic;
using System.ServiceModel;
using WcfCalculatorClient.CalculatorServiceReference;

namespace WcfCalculatorClient
{
    class Program
    {
        private Dictionary<String, Action<Double>> oneArgumentOperations;

        static void Main(string[] args)
        {
            using (TestClient client = new TestClient())
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Enter operation:");
                        string operation = Console.ReadLine();

                        int argumentNumber = client.GetArgumentNumber(operation);
                        double[] arguments= ReadArgs(argumentNumber);
                        double result = client.PerformCalculation(operation, arguments);
                        Console.WriteLine("Result: " + result);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid argument value.");
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Operation is not supported.");
                    }
                    catch (FaultException<CalculationFault> fault)
                    {
                        Console.WriteLine(fault.Detail.Message);
                    }
                    catch (CommunicationException)
                    {
                        Console.WriteLine("Connection problems");
                    }
                    Console.WriteLine();
                }
            }
        }

        private static double[] ReadArgs(int argumentNumber)
        {
            double[] args = new double[argumentNumber];

            for (int i = 0; i < argumentNumber; i++)
            {
                Console.WriteLine("Enter argument " + (i + 1) + ":");
                args[i] = Double.Parse(Console.ReadLine());
            }

            return args;
        }
    }
}
