using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace BankMock
{
    class Program
    {
        static void Main(string[] args)
        {
            var stub = FluentMockServer.Start(new FluentMockServerSettings
            {
                Urls = new[] { "http://+:5001" },
                StartAdminInterface = true
            });

            // Reject payments with an amount of 10
            stub
            .Given(
            Request.Create().WithPath("/payment/authorize").WithBody(new JsonPathMatcher("$..[?(@.amount == 10)]")).UsingPost()
            ).AtPriority(1)
            .RespondWith(
                Response.Create()
                .WithStatusCode(500)
                .WithHeader("Content-Type", "application/json")
                .WithBodyAsJson(new
                {
                    Success = false,
                    TransactionCode = "",
                    UserMessage = "Could not authorize payment."
                }));

            // Authorize (with delay) payments of 20
            stub
            .Given(
            Request.Create().WithPath("/payment/authorize").WithBody(new JsonPathMatcher("$..[?(@.amount == 20)]")).UsingPost()
            ).AtPriority(10)
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBodyAsJson(new
                {
                    Success = true,
                    TransactionCode = "{{Random Type=\"Guid\"}}",
                    UserMessage = ""
                }).WithTransformer().WithDelay(TimeSpan.FromSeconds(10)));

            // Authorize payment
            stub
            .Given(
            Request.Create().WithPath("/payment/authorize").UsingPost()
            ).AtPriority(100)
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBodyAsJson(new
                {
                    Success = true,
                    TransactionCode = "{{Random Type=\"Guid\"}}",
                    UserMessage = ""
                }).WithTransformer());

            // Return 404 if other request
            stub
            .Given(
            Request.Create().WithPath("/*").UsingPost()
            ).AtPriority(1000)
            .RespondWith(
                Response.Create()
                .WithStatusCode(404)
                .WithHeader("Content-Type", "application/json")
                .WithBody("This is not a valid API operation."));

            Console.WriteLine("Press any key to stop the server");
            Console.ReadKey();

            Console.WriteLine("Displaying all requests");
            var allRequests = stub.LogEntries;
            Console.WriteLine(JsonConvert.SerializeObject(allRequests, Formatting.Indented));

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
            stub.Stop();
        }
    }
}
