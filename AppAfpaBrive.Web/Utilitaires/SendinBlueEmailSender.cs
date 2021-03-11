using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using Microsoft.Extensions.Configuration;

namespace AppAfpaBrive.Web.Utilitaires
{
    public class SendinBlueEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        public SendinBlueEmailSender(IConfiguration config)
        {
            _config = config;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
         //   return ExecuteSMTP(subject, htmlMessage, email);
            return ExecuteAPI(subject, htmlMessage, email);
        }
        public Task ExecuteSMTP(string subject, string message, string email)
        {
            
            var configuration = _config.GetSection("Mailer").Get<MailerSettings>();
            var client = new System.Net.Mail.SmtpClient(configuration.SmtpRelay, configuration.SmtpPort);
            client.Credentials = new NetworkCredential(configuration.Credentials_Id, configuration.Credentials_Pw);
            client.UseDefaultCredentials = false;

            var msg = new MailMessage
            {
                From = new MailAddress(configuration.MailFrom, configuration.NameFrom, System.Text.UTF8Encoding.UTF8),
                Subject = subject,
                Body = message,
                IsBodyHtml = false
            };
            msg.To.Add(email);
            return client.SendMailAsync(msg);
        }
        public Task ExecuteAPI(string subject, string message, string email)
        {
            var configuration = _config.GetSection("Mailer").Get<MailerSettings>();
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.Subject = subject;
            sendSmtpEmail.TextContent = message;
            sendSmtpEmail.Sender = new SendSmtpEmailSender(configuration.NameFrom, configuration.MailFrom);
            sendSmtpEmail.To = new List<SendSmtpEmailTo>();
            sendSmtpEmail.To.Add(new SendSmtpEmailTo(email));

            Func<SendSmtpEmail, CreateSmtpEmail> send = (mail) =>
            {
                Configuration.Default.AddApiKey("api-key", configuration.ServiceApiKey);
                var apiInstance = new TransactionalEmailsApi();
                return apiInstance.SendTransacEmail(mail);
            };
            var TEnvoi = Task.Factory.StartNew(() => send(sendSmtpEmail));
            
            return TEnvoi;


        }
    }
}

