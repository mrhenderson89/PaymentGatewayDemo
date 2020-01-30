/// ----------------------------------------------------------------------
/// <summary>
/// Defines the PaymentService class.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Services
{
    using System;
    using System.Threading.Tasks;
    using Interfaces;
    using PaymentGatewayDemo.APIClients.Interfaces;
    using PaymentGatewayDemo.Enums;
    using PaymentGatewayDemo.Logging;
    using PaymentGatewayDemo.Models;
    using PaymentGatewayDemo.Models.ViewModels;
    using PaymentGatewayDemo.Repositories.Interfaces;

    public class PaymentService : IPaymentService
    {
        #region Fields

        /// <summary>
        /// the Logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// the Payment Repository.
        /// </summary>
        private readonly IPaymentRepository paymentRepository;

        /// <summary>
        /// the Bank API Client
        /// </summary>
        private readonly IBankAPIClient bankAPIClient;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises the Order controller.
        /// </summary>
        public PaymentService(ILogger logger, IPaymentRepository paymentRepository, IBankAPIClient bankClient)
        {
            this.logger = logger;
            this.paymentRepository = paymentRepository;
            this.bankAPIClient = bankClient;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the Payment.
        /// </summary>
        public async Task<PaymentResultViewModel> CreatePaymentAsync(PaymentCardDetailsInputViewModel paymentDetails)
        {
            try
            {
                var paymentEntity = this.paymentRepository.Add<Payment, int>(new Payment
                {
                    PaymentCode = Guid.NewGuid(),
                    PaymentStatusId = (int)PaymentStatus.Created,
                    Amount = paymentDetails.Amount,
                    CurrencyId = paymentDetails.CurrencyId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                });

                var cardDetails = this.paymentRepository.Add<PaymentCardDetails, int>(new PaymentCardDetails
                {
                    PaymentCode = paymentEntity.PaymentCode,
                    CardNumber = paymentDetails.CardLast4Digits,
                    ExpiryMonth = paymentDetails.ExpiryMonth,
                    ExpiryYear = paymentDetails.ExpiryYear,
                    CVV = paymentDetails.CVV
                });

                var bankResult = await this.bankAPIClient.InitiatePaymentAuthorizationAsync(new BankAPIRequest
                {
                    CardNumber = cardDetails.CardNumber,
                    ExpiryMonth = cardDetails.ExpiryMonth,
                    ExpiryYear = cardDetails.ExpiryYear,
                    Amount = paymentDetails.Amount,
                    Currency = ((Currency)paymentDetails.CurrencyId).ToString(),
                    CVV = cardDetails.CVV
                });

                if (bankResult.Success)
                {
                    paymentEntity.BankTransactionCode = bankResult.TransactionCode;
                    paymentEntity.PaymentStatusId = (int)PaymentStatus.Complete;                    
                }
                else
                {
                    paymentEntity.PaymentStatusId = (int)PaymentStatus.Failed;
                    this.logger.LogError(string.Format("Could not authorize payment {0}", paymentEntity.PaymentCode));
                }

                paymentEntity.UpdatedDate = DateTime.Now;
                paymentEntity = this.paymentRepository.Update(paymentEntity, p => p.Id);

                return new PaymentResultViewModel
                {
                    PaymentCode = paymentEntity.PaymentCode,
                    PaymentStatus = ((PaymentStatus)paymentEntity.PaymentStatusId).ToString(),
                    CardNumber = cardDetails.CardNumber,
                    ExpiryMonth = cardDetails.ExpiryMonth,
                    ExpiryYear = cardDetails.ExpiryYear,
                    CVV = cardDetails.CVV,
                    UserMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                //Log error message
                this.logger.LogFatal(ex, string.Format("Could not authorize payment {0}", paymentDetails.CardLast4Digits));

                return new PaymentResultViewModel
                {
                    PaymentStatus = PaymentStatus.Error.ToString(),
                    UserMessage = "Could not create Payment at this time."
                };
            }
        }

        /// <summary>
        /// Gets the Payment by PaymentCode.
        /// </summary>
        public PaymentResultViewModel GetPayment(Guid paymentCode)
        {
            PaymentResultViewModel paymentResult = new PaymentResultViewModel
            {
                PaymentStatus = PaymentStatus.Failed.ToString()
            };

            try
            {
                var paymentCardDetails = this.paymentRepository.GetPaymentWithCardDetails(paymentCode);

                if (paymentCardDetails != null)
                {
                    paymentResult.PaymentCode = paymentCardDetails.PaymentCode;
                    paymentResult.PaymentStatus = paymentCardDetails.PaymentStatus.ToString();
                    paymentResult.CardNumber = paymentCardDetails.CardDetails.CardNumber;
                    paymentResult.ExpiryMonth = paymentCardDetails.CardDetails.ExpiryMonth;
                    paymentResult.ExpiryYear = paymentCardDetails.CardDetails.ExpiryYear;
                    paymentResult.CVV = paymentCardDetails.CardDetails.CVV;
                }
                else
                {
                    paymentResult.UserMessage = "No payment found matching this ID";
                }

                return paymentResult;

            }
            catch (Exception ex)
            {
                paymentResult.UserMessage = "Could not retrieve payment at this time";
                return paymentResult;
            }
        }

        #endregion

    }
}