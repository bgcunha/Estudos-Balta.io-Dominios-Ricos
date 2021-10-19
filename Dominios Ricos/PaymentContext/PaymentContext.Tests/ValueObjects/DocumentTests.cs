using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Invalid, "");            
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCNPJIsValid()
        {
            var doc = new Document("19221783000161", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
        }
        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CPF);
            Assert.IsTrue(doc.Invalid, "");
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("06755995983")]
        [DataRow("62050023022")]
        [DataRow("29369703004")]
        //[DataRow("01213")]     
        public void ShouldReturnSuccessWhenCPFIsValid(string cpf)
        {
            var doc = new Document(cpf, EDocumentType.CPF);
            Assert.IsTrue(doc.Valid, "");
        }
    }
}

    