using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Projet_Test.IMailSenderMock
{
    public class IMailSenderMock : IEmailSender
    {
        public IMailSenderMock() { }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Vous devez spécifier l'adresse mail",
                    nameof(email));

            if (!IsEmailValid(email))
                throw new ArgumentException("Vous devez spécifier un courriel valide",
                    nameof(email));

            if (string.IsNullOrEmpty(subject))
                throw new ArgumentException("Vous devez spécifier le sujet du courriel",
                    nameof(subject));

            if (string.IsNullOrEmpty(htmlMessage))
                throw new ArgumentException("Vous devez spécifier le corps du courriel",
                    nameof(htmlMessage));
            await Task.Delay(1);
        }

        protected bool IsEmailValid(string courriel)
        {
            return new System.ComponentModel.DataAnnotations
                        .EmailAddressAttribute()
                        .IsValid(courriel);
        }
    }
}
