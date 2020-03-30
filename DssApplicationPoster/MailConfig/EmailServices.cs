using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DssApplicationPoster.MailConfig
{
    public class EmailServices : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailServices(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public List<EmailMessage> ReceiveEmail(int maxCount = 100)
        {
            using (var emailClient = new Pop3Client())
            {
                emailClient.Connect(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);

                List<EmailMessage> emails = new List<EmailMessage>();
                for (int i = 0; i < emailClient.Count && i < maxCount; i++)
                {
                    var message = emailClient.GetMessage(i);
                    var emailMessage = new EmailMessage
                    {
                        Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                        Subject = message.Subject
                    };

                    emailMessage.ToAddress.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emailMessage.FromAddress.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emails.Add(emailMessage);
                }

                return emails;
            }
        }

        public void SendMail(EmailMessage emailMessage)
        {
            MimeMessage mailMessage = new MimeMessage();
            mailMessage.To.AddRange(emailMessage.ToAddress.Select(to => new MailboxAddress(to.Name, to.Address)));
            mailMessage.From.AddRange(emailMessage.FromAddress.Select(from => new MailboxAddress(from.Name, from.Address)));
            mailMessage.Subject = emailMessage.Subject;
            mailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailMessage.Content
            };


            // Setting SMTP Client
            using (SmtpClient mailClient = new SmtpClient())
            {
                //The last parameter here is to use SSL (Which you should!)
                mailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, MailKit.Security.SecureSocketOptions.SslOnConnect);

                //Remove any OAuth functionality as we won't be using it. 
                mailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                mailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                mailClient.Send(mailMessage);
                mailClient.Disconnect(true);
            }
        }
    }
}
