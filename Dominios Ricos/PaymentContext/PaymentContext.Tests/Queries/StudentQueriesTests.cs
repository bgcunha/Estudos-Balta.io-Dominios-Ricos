
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _students;

        public StudentQueriesTests()
        {
            for (int i = 0; i < 10; i++)
            {
                _students.Add(new Student(
                    new Name("Aluno", i.ToString()),
                    new Document("111111111" + i, EDocumentType.CPF),
                    new Email(i + "@gmail.com")
                ));
            }
        }
        
        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists()
    	{
            var exp = StudentQueries.GetStudentInfo("12345678911");
            var studn = _students.AsQueryable().Where(exp).FirstOrDefault();
            Assert.AreEqual(null, studn);
        }

        [TestMethod]
        public void ShouldReturnStudentWhenDocumenExists()
    	{
            var exp = StudentQueries.GetStudentInfo("1111111111");
            var studn = _students.AsQueryable().Where(exp).FirstOrDefault();
            Assert.AreNotEqual(null, studn);
        }
    }
    
}