using CathedralKitchen.NewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CathedralKitchen.Services
{
    public static class EmailNotificationService
    {
        public static void Mailer(Person person, dynamic title, dynamic messageToBePassed)
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
            EmailService.Send(message);
        }

        public static void SendMail(Person person)
        {
            var addressFrom = new List<EmailAddress>
            {
                new EmailAddress {Address = "noreply@miviewis.com", Name = "🔥🔥MiView Squad Fam🔥🔥 👌😏"}
            };
            var address = new List<EmailAddress> { new EmailAddress { Address = person.Email, Name = person.FirstName } };
            var message = new EmailMessage
            {
                ToAddresses = address,
                FromAddresses = addressFrom,
                Content =
                    $"This is a 🔥fire🔥 test message from the 🔥LIT🔥 mailer service. <br /> <br /> ⛔Don't⛔ forget to ✔check✔ my 🎶soundcloud🎶 for my latest 💯mixtape💯 it is 🔥fire🔥 ❌HATERS❌ 😤OUT😤  <br /> <br /> <br /> <br />  Smell ya later, <br />  🍤Lil' Shrimp🍤",
                Subject = Convert.ToString($"Hello, {person.FirstName}")
            };
            EmailService.Send(message);
        }
    }
}
