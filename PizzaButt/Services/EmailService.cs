using CathedralKitchen.NewModels;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CathedralKitchen.Services
{

    public interface IEmailService
    {
        void Send(EmailMessage message);
    }

    public class EmailService : IEmailService
    {
        private IEmailConfiguration _emailConfiguration;
        public enum Types
        {
            PasswordReset
        };

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public List<EmailMessage> ReceiveEmail(int maxCount = 10)
        {
            using (var emailClient = new Pop3Client())
            {
                emailClient.Connect("pop.gmail.com", 995, true);

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate("Chaserbullock@gmail.com", "");

                var emails = new List<EmailMessage>();
                for (var i = 0; i < emailClient.Count && i < maxCount; i++)
                {
                    var message = emailClient.GetMessage(i);
                    var emailMessage = new EmailMessage
                    {
                        Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                        Subject = message.Subject
                    };
                    emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                }

                return emails;
            }
        }

        public void Send(EmailMessage emailMessage)
        {

            var message = new MimeMessage();
            message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

            message.Subject = emailMessage.Subject;
            //We will say we are sending HTML. But there are options for plaintext etc.
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailMessage.Content
            };



            //Be careful that the SmtpClient class is the one from Mailkit not the framework!
            using (var emailClient = new SmtpClient())
            {
                emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                //The last parameter here is to use SSL (Which you should!)
                emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, false);

                //Remove any OAuth functionality as we won't be using it.
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

                emailClient.Send(message);

                emailClient.Disconnect(true);
            }
        }

        //public static void Send(string content, string recipient, string name, Types type)
        //{
        //    switch (type)
        //    {
        //        case Types.PasswordReset:
        //            var msg = new EmailMessage
        //            {
        //                Content = @"You or someone using your Email Address has recently requested a password reset. Here is your temporary password:<br/>" +
        //                            content +
        //                            "<br/>If this was not you, we recommend you contact your Systems Administrator immediately.<br/><br/>" +
        //                            "- The MiView Team",
        //                ToAddresses = new List<EmailAddress>
        //                {
        //                    new EmailAddress
        //                    {
        //                        Address = recipient,
        //                        Name = name
        //                    }
        //                },
        //                Subject = "MiView Systems Password Reset",
        //                FromAddresses = new List<EmailAddress>
        //                {
        //                    new EmailAddress
        //                    {
        //                        Address = "noreply@miviewis.com",
        //                        Name = "MiView Integrated Systems"
        //                    }
        //                }
        //            };
        //            Send(msg);
        //            break;
        //        default:
        //            return;
        //    }
    }
}
