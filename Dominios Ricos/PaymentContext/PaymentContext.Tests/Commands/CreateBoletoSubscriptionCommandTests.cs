using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domais.Commands;

namespace PaymentContext.Tests
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "";

            command.Validate();
            Assert.AreEqual(false, command.Valid, "NName.FirstName", "Nome Inválido.");            
        }
    }
}
