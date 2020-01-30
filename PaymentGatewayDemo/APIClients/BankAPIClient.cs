/// ----------------------------------------------------------------------
/// <summary>
/// Defines the BankAPIClient class.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.APIClients
{
    using System;
    using System.Configuration;
    using System.Threading.Tasks;
    using Interfaces;
    using PaymentGatewayDemo.Models;
    using RestSharp;

    /// <summary>
    /// Bank API Client
    /// </summary>
    public class BankAPIClient : IBankAPIClient
    {
        string BaseUrl;

        readonly IRestClient _client;

        /// <summary>
        /// Bank API constructor
        /// </summary>
        public BankAPIClient()
        {
            BaseUrl = ConfigurationManager.AppSettings["BankAPIBaseURL"]; 
            _client = new RestClient(BaseUrl);
        }

        /// <summary>
        /// The execute async
        /// </summary>
        /// <param name="request">
        /// The request
        /// </param>
        public T ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var response = _client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var bankAPIException = new Exception(message, response.ErrorException);
                throw bankAPIException;
            }
            return response.Data;
        }

        /// <summary>
        /// The initiate payment authorization async
        /// </summary>
        /// <param name="body">
        /// The request
        /// </param>
        public async Task<BankAPIResponse> InitiatePaymentAuthorizationAsync(BankAPIRequest body)
        {
            var request = new RestRequest("payment/authorize", Method.POST);
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(new {
                cardnumber = body.CardNumber,
                expirymonth = body.ExpiryMonth,
                expiryyear = body.ExpiryYear,
                amount = body.Amount,
                currency = body.Currency,
                cvv = body.CVV
            });

            return ExecuteAsync<BankAPIResponse>(request);
        }
    }
}