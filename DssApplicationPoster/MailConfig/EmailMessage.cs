using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DssApplicationPoster.MailConfig
{
    public class EmailMessage
    {
        public List<EmailAddress> ToAddress { get; set; }
        public List<EmailAddress> FromAddress { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public EmailMessage()
        {
            ToAddress = new List<EmailAddress>();
            FromAddress = new List<EmailAddress>();
        }
    }
}
