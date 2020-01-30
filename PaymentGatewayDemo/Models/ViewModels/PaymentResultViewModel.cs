/// ----------------------------------------------------------------------
/// <summary>
/// Defines the PaymentResultViewModel model.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Models.ViewModels
{
    using System;

    public class PaymentResultViewModel
    {
        /// <summary>
        /// The payment code
        /// </summary>
        public Guid PaymentCode { get; set; }

        /// <summary>
        /// The payment status
        /// </summary>
        public string PaymentStatus { get; set; }

        /// <summary>
        /// The card number
        /// </summary>
        public int CardNumber { get; set; }

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

        /// <summary>
        /// The user message
        /// </summary>
        public string UserMessage { get; set; }
    }
}