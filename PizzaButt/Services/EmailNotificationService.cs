using CathedralKitchen.NewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CathedralKitchen.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly IEmailService _emailService;

        public EmailNotificationService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public enum Status { Acknowledged, Complete }

        public void Mailer(Person person, dynamic title, dynamic messageToBePassed)
        {
            var addressFrom = new List<EmailAddress>
            {
                new EmailAddress
                {
                    Address = "noreply@miviewis.com",
                    Name = "🔥🔥MiView Squad Fam🔥🔥 👌😏"
                }
            };
            var address = new List<EmailAddress>
            {
                new EmailAddress
                {
                     Address = person.Email,
                     Name = person.FirstName
                }
            };
            var message = new EmailMessage
            {
                ToAddresses = address,
                FromAddresses = addressFrom,
                Content = $"{messageToBePassed}",
                Subject = Convert.ToString($"Hello, {person} {title}")
            };
            _emailService.Send(message);
        }

        public void SendMail(Person person, int status)
        {
            var content = "";
            switch ((Status)status)
            {
                case Status.Acknowledged:
                    content = "Your order has been acknowledged. We'll let you know when it is done!";
                    break;
                case Status.Complete:
                    content = "Your order is complete!";
                    break;

                default:
                    break;
            }
            var addressFrom = new List<EmailAddress>
            {
                new EmailAddress {Address = "noreply@CathedralBites.com", Name = "Cathedral Bites"}
            };
            var address = new List<EmailAddress> { new EmailAddress { Address = person.Email, Name = person.FirstName } };
            var message = new EmailMessage
            {
                ToAddresses = address,
                FromAddresses = addressFrom,
                Content = content,
                Subject = Convert.ToString($"Hello, {person.FirstName}")
            };
            _emailService.Send(message);
        }
    }
}
