/// ----------------------------------------------------------------------
/// <summary>
/// Defines the AddPayment tests.
/// </summary>
/// ----------------------------------------------------------------------

namespace PaymentGatewayDemo.Tests.Controllers.PaymentControllerFixtures
{
    using Moq;
    using NUnit.Framework;
    using PaymentGatewayDemo.Models.ViewModels;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Add Payment Tests.
    /// </summary>
    [TestFixture]
    public class AddPaymentTests : TestBaseInitialiser
    {
        /// <summary>
        /// Should Return BadRequest if null payment.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfPaymentNull()
        {
            // Arrange
            var controller = this.InitPaymentController();

            // Act
            var result = controller.AddPayment(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if negative amount.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfAmountNegative()
        {
            // Arrange
            var controller = this.InitPaymentController();
            PaymentCardDetailsInputViewModel payment = new PaymentCardDetailsInputViewModel()
            {
                Amount = -1
            };

            // Act
            var result = controller.AddPayment(payment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if invalid currency.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfInvalidCurrency()
        {
            // Arrange
            var controller = this.InitPaymentController();
            PaymentCardDetailsInputViewModel payment = new PaymentCardDetailsInputViewModel()
            {
                Amount = 1,
                Currency = "USD"
            };

            // Act
            var result = controller.AddPayment(payment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if short Card Number.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfShortCardNumber()
        {
            // Arrange
            var controller = this.InitPaymentController();
            PaymentCardDetailsInputViewModel payment = new PaymentCardDetailsInputViewModel()
            {
                Amount = 1,
                Currency = "GBP",
                CardNumber = "12345"
            };

            // Act
            var result = controller.AddPayment(payment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if long Card Number.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfLongCardNumber()
        {
            // Arrange
            var controller = this.InitPaymentController();
            PaymentCardDetailsInputViewModel payment = new PaymentCardDetailsInputViewModel()
            {
                Amount = 1,
                Currency = "GBP",
                CardNumber = "12345678123456789"
            };

            // Act
            var result = controller.AddPayment(payment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if invalid Card Number.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfInvalidCardNumber()
        {
            // Arrange
            var controller = this.InitPaymentController();
            PaymentCardDetailsInputViewModel payment = new PaymentCardDetailsInputViewModel()
            {
                Amount = 1,
                Currency = "GBP",
                CardNumber = "ABCDEABCDEABCDEA"
            };

            // Act
            var result = controller.AddPayment(payment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if invalid Expiry Month.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfInvalidExpiryMonth()
        {
            // Arrange
            var controller = this.InitPaymentController();
            PaymentCardDetailsInputViewModel payment = new PaymentCardDetailsInputViewModel()
            {
                Amount = 1,
                Currency = "GBP",
                CardNumber = "1234567812345678",
                ExpiryMonth = 13
            };

            // Act
            var result = controller.AddPayment(payment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if invalid Expiry Year.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfInvalidExpiryYear()
        {
            // Arrange
            var controller = this.InitPaymentController();
            PaymentCardDetailsInputViewModel payment = new PaymentCardDetailsInputViewModel()
            {
                Amount = 1,
                Currency = "GBP",
                CardNumber = "1234567812345678",
                ExpiryMonth = 12,
                ExpiryYear = 18
            };

            // Act
            var result = controller.AddPayment(payment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if invalid CVV.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfInvalidCVV()
        {
            // Arrange
            var controller = this.InitPaymentController();
            PaymentCardDetailsInputViewModel payment = new PaymentCardDetailsInputViewModel()
            {
                Amount = 1,
                Currency = "GBP",
                CardNumber = "1234567812345678",
                ExpiryMonth = 12,
                ExpiryYear = 28,
                CVV = 1111
            };

            // Act
            var result = controller.AddPayment(payment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if invalid CVV.
        /// </summary>
        [Test]
        public void ShouldReturnOKIfValidRequest()
        {
            // Arrange
            var controller = this.InitPaymentController();
            PaymentCardDetailsInputViewModel payment = new PaymentCardDetailsInputViewModel()
            {
                Amount = 1,
                Currency = "GBP",
                CardNumber = "1234567812345678",
                ExpiryMonth = 12,
                ExpiryYear = 28,
                CVV = 111
            };

            this.PaymentServiceMock.Setup(x => x.CreatePaymentAsync(It.IsAny<PaymentCardDetailsInputViewModel>())).Returns(Task.FromResult(default(PaymentResultViewModel)));

            // Act
            var result = controller.AddPayment(payment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
