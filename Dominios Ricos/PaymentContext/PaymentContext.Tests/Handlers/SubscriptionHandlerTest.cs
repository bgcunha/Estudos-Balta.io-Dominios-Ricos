using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domais.Commands;
using PaymentContext.Tests.Mock;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTest
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
    	{
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            
            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999999";
            command.Email = "hello@gmail.com";        
            command.BarCode = "123456798";
            command.BoletoNumber = "123456789";         
            command.PaymentNumber = "1231212";
            command.PaidDate = System.DateTime.Now;
            command.ExpireDate= System.DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "WAYNE CORP";
            command.PayerDocument = "12345678911";
            command.PayerDocumentType = Domain.Enums.EDocumentType.CPF;
            command.PayerEmail = "batman@dc.com";
            command.Street = "asdf";
            command.Number = "asdf";
            command.Neighborhood = "asdf";
            command.City = "asdf";
            command.State = "as";
            command.Country = "asdfd";
            command.ZipCode = "86360000";
            command.Phone = "121354646";
            
            handler.Handle(command);

            Assert.AreEqual(false, handler.Valid);
        }        
        
    }
}