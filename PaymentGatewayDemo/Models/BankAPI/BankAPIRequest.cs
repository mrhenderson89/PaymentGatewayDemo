/// ----------------------------------------------------------------------
/// <summary>
/// Defines the BankAPIRequest model.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Models
{
    public class BankAPIRequest
    {
        /// <summary>
        /// The card number
        /// </summary>
        public int CardNumber { get; set; }

        /// <summary>
        /// The expiry month
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// The expiry year
        /// </summary>
        public int ExpiryYear { get; set; }

        /// <summary>
        /// The amount
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// The currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// The CVV
        /// </summary>
        public int CVV { get; set; }
    }
}