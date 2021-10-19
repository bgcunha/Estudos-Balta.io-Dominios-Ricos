
using System;
using Flunt.Notifications;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domais.Commands;
using PaymentContext.Domais.Services;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
    Notifiable,
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>,
    IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validations
            command.Validate();
            if(command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");
            }
                

            //Verificar se documenta está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document","Esse CPF já está em uso");

            //Verificar se email está cadastrado
             if (_repository.EmailExists(command.Document))
                AddNotification("Document","Esse CE-mail já está em uso");

            //Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode, command.Phone);
           

            //Gerar entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(System.DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                                            command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate, 
                                            command.Total, command.TotalPaid,command.Payer, 
                                            new Document("12345678912", EDocumentType.CPF), address, email
                                           );
            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            if(Invalid)
                return  new CommandResult(true, "Não foi possivel realizar sua assinatura");
            //Salvar as informações
            _repository.CreateSubscription(student);
            
            //Enviar email
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Sua assinaura foi criada" );

            //Retornar as informações
            return  new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            // command.Validate();
            // if(command.Invalid)
            // {
            //     AddNotifications(command);
            //     return new CommandResult(false, "Não foi possivel realizar sua assinatura");
            // }
                

            //Verificar se documenta está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document","Esse CPF já está em uso");

            //Verificar se email está cadastrado
             if (_repository.EmailExists(command.Document))
                AddNotification("Document","Esse CE-mail já está em uso");

            //Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode, command.Phone);
           

            //Gerar endidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(System.DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                                            command.TransactioCode, command.PaidDate, command.ExpireDate, 
                                            command.Total, command.TotalPaid,command.Payer, 
                                            new Document("12345678912", EDocumentType.CPF), address, email
                                           );
            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar as informações
            _repository.CreateSubscription(student);
            
            //Enviar email
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Sua assinaura foi criada" );

            //Retornar as informações
            return  new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            //Verificar se documenta está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document","Esse CPF já está em uso");

            //Verificar se email está cadastrado
             if (_repository.EmailExists(command.Document))
                AddNotification("Document","Esse CE-mail já está em uso");

            //Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode, command.Phone);
           

            //Gerar endidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(System.DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(
                                            command.CardHolderName,command.CardNumber, DateTime.Now.ToString(), command.PaidDate, command.ExpireDate, 
                                            command.Total, command.TotalPaid,command.Payer, 
                                            new Document("12345678912", EDocumentType.CPF), address, email
                                           );
            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar as informações
            _repository.CreateSubscription(student);
            
            //Enviar email
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Sua assinaura foi criada" );

            //Retornar as informações
            return  new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}