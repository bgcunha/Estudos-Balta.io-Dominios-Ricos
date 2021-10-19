using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        private IList<Payment> _payments;
        public Subscription(DateTime? expireDate)
        {
            CreatDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            Active = true;
            _payments = new List<Payment>();
        }
        public DateTime CreatDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool Active { get; private set; }

        public IReadOnlyCollection<Payment> Payments { get{return _payments.ToArray();} private set{} }

        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsLowerThan(DateTime.Now, payment.ExpireDate, "Subscription.Payments","A data do pagamento não pode estar no passado")
            );

            //if(Valid)
            _payments.Add(payment);
        }
        public bool IsActive()
        {
            return Active;
        }

        public void Activate()
        {
            Active = true;
            LastUpdateDate = DateTime.Now;        }

        public void Inactivate()
        {
           Active = false;
           LastUpdateDate = DateTime.Now;
        }
    }
} 