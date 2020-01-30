/// ----------------------------------------------------------------------
/// <summary>
/// Defines the BankAPIResponse model.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Models
{
    public class BankAPIResponse
    {
        /// <summary>
        /// The success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The payment ID
        /// </summary>
        public string TransactionCode { get; set; }

        /// <summary>
        /// The user message
        /// </summary>
        public string UserMessage { get; set; }
    }
}