using System;
using System.ServiceModel;
using WcfCalculatorClient.CalculatorServiceReference;

namespace WcfCalculatorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculatorClient client = new CalculatorClient();
            try
            {
                Console.WriteLine(client.Sqrt(-9));
            }
            catch (FaultException<CalculationFault> fault)
            {
                Console.WriteLine(fault.Detail.Message);
            }
            client.Close();
            Console.ReadLine();
        }
    }
}
