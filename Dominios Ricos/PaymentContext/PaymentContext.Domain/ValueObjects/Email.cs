using Flunt.Validations;
using PaymentContext.Shared.ValueObject;

namespace PaymentContext.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract()
                            .Requires()
                            .IsEmail(Address, "Email.Address", "E-mail Inválido")
                        );
        }

        public string Address { get; private set; }        
    }
}