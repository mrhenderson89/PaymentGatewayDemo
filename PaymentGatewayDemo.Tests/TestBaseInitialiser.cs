/// ----------------------------------------------------------------------
/// <summary>
/// Defines the test base initialiser.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Tests
{
    using Moq;
    using NUnit.Framework;
    using PaymentGatewayDemo.APIClients.Interfaces;
    using PaymentGatewayDemo.Logging;
    using PaymentGatewayDemo.Repositories.Interfaces;
    using PaymentGatewayDemo.Services.Interfaces;

    /// <summary>
    /// The test base initialiser.
    /// </summary>
    [TestFixture]
    public abstract class TestBaseInitialiser : TestBase
    {
        /// <summary>
        /// Set up this test instance.
        /// </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.LoggerMock = new Mock<ILogger>(MockBehavior.Strict);
            this.PaymentRepositoryMock = new Mock<IPaymentRepository>(MockBehavior.Strict);
            this.PaymentServiceMock = new Mock<IPaymentService>(MockBehavior.Strict);
            this.BankAPIMock = new Mock<IBankAPIClient>(MockBehavior.Strict);
        }

        /// <summary>
        /// Tear down this test instance.
        /// </summary>
        [TearDown]
        public void AfterTest()
        {
            this.LoggerMock.VerifyAll();
            this.PaymentRepositoryMock.VerifyAll();
            this.PaymentServiceMock.VerifyAll();
            this.BankAPIMock.VerifyAll();
        }
    }
}
