/// ----------------------------------------------------------------------
/// <summary>
/// Defines the BankAPIClient interface.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.APIClients.Interfaces
{
    using PaymentGatewayDemo.Models;
    using RestSharp;
    using System.Threading.Tasks;

    /// <summary>
    /// Bank API Client
    /// </summary>
    public interface IBankAPIClient
    {
        /// <summary>
        /// The execute async
        /// </summary>
        /// <param name="request">
        /// The request
        /// </param>
        T ExecuteAsync<T>(RestRequest request) where T : new();

        /// <summary>
        /// The initiate payment authorization async
        /// </summary>
        /// <param name="body">
        /// The request
        /// </param>
        Task<BankAPIResponse> InitiatePaymentAuthorizationAsync(BankAPIRequest body);
    }
}