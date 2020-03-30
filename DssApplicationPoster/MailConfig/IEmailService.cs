using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DssApplicationPoster.MailConfig
{
    public interface IEmailService
    {
        void SendMail(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 100);
    }
}
