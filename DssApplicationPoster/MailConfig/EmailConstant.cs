using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DssApplicationPoster.MailConfig
{
    public static class EmailConstant
    {
        // Setting SMTP Server
        public static readonly string SmtpServer = "Your STMP Or POP3 SERVER"; // Recomend Use STMP Server
        public static readonly int SmtpPort = "Your Port STMP or POP3 Mail Server."; // Data Type Integer
        public static readonly string SmtpUsername = "Your-Email";
        public static readonly string SmtpPassword = "Your-Password";

        // Setting Email To
        public static readonly string EmailAddress = "Your-Email";
        public static readonly string Username = "Your-Username";
        public static readonly string Password = "Your-Password";

        public static string EmailTemplateHtml(string name, string attend, string guests, string description)
        {
            return $@"<!DOCTYPE html> <html> <head> <meta charset='utf-8'> <meta name='viewport' content='width=device-width, initial-scale=1.0, shrink-to-fit=no'> <title>{attend}</title> <link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.3.1/css/bootstrap.min.css'> </head> <body> <div class='container shadow'> <div class='row'> <div class='col'> <div class='card'> <div class='card-header bg-primary'> <h1 class='mb-0'>{String.Concat(attend, " ", "[" + name + "]")}</h1> </div><div class='card-body'> <h5 class='card-title mb-0'>Fullname : {name}</h5> <h5 class='card-title mb-0'>Present Or Not : {attend}</h5> <h5 class='card-title mb-0'>Number Of Guests : {guests}</h5> <div class='row'> <div class='col'> <p>{description}</p></div></div></div></div></div></div></div></body> </html>";
        }

        public static string AcctivationAccount(string preferredName, string urlSchema, string urlAuthhory, int userId, string accountCode)
        {
            return "<h1>Activation Your Email Account</h1>" +
                    "<br />" +
                    $"<h3> Welcome, {preferredName}.</h3 >" +
                    "<p> Please click the following link to activate your account, <a href='"+ string.Format("{0}://{1}/Administrator/PrivilegedData/AccountActivation?accountCode={2}&confirmAccount={3}", urlSchema, urlAuthhory, userId, accountCode) +"'>Click here to activate.</a></p>" +
                    "<br />" +
                    "<p>Thanks by Administrator</p>";
        }

        public static string AcctivationAccount(string preferredName, string urlCallback)
        {
            return "<h1>Activation Your Email Account</h1>" +
                    "<br />" +
                    $"<h3> Welcome, {preferredName}.</h3 >" +
                    "<p> Please click the following link to activate your account, <a href=\""+ urlCallback +"\">Click here to activate.</a></p>" +
                    "<br />" +
                    "<p>Thanks by Administrator</p>";
        }

        public static string ForgotPassword(string username, string urlSchema, string urlAuthhory, string userCode, string usernameEncrypt, string emailEncrypt)
        {
            return "<h1>Reset Your Password</h1>" +
                   "<br />" +
                   $"<h3> Hi, {username}.</h3 >" +
                   "<p> Please click the following link to create new password, <a href='" + string.Format("{0}://{1}/Administrator/PrivilegedData/ConfirmForgotPassword?userCode={2}&usernameCode={3}&emailCode={4}", urlSchema, urlAuthhory, userCode, usernameEncrypt, emailEncrypt) + "'>Click here to reset password.</a></p>" +
                   "<br />" +
                   "<p>Thanks by Administrator</p>";
        }
    }
}
