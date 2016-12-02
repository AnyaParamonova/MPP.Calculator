using System;
using WcfCalculatorClient.CalculatorServiceReference;

namespace WcfCalculatorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculatorClient client = new CalculatorClient();
            Console.WriteLine(client.Sqrt(45));
            client.Close();
            Console.ReadLine();
        }
    }
}
