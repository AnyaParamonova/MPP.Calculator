using System.Runtime.Serialization;

namespace WcfCalculatorService
{
    [DataContract]
    public class CalculationFault
    {
        [DataMember]
        public string Message { get; set; }

        public CalculationFault(string message)
        {
            this.Message = message;
        }
    }
}