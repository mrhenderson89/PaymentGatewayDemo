/// ----------------------------------------------------------------------
/// <summary>
/// Defines the PaymentCardDetailsInputViewModel model.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Models.ViewModels
{
    using System.Runtime.Serialization;

    public class PaymentCardDetailsInputViewModel
    {
        /// <summary>
        /// The amount
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// The currency Id
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// The currency Id
        /// </summary>
        [IgnoreDataMember]
        public int CurrencyId { get; set; }

        /// <summary>
        /// The card number
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// The card number
        /// </summary>
        [IgnoreDataMember]
        public int CardLast4Digits { get; set; }

        /// <summary>
        /// The card expiry month
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// The card expiry year
        /// </summary>
        public int ExpiryYear { get; set; }

        /// <summary>
        /// The CVV
        /// </summary>
        public int CVV { get; set; }
    }
}