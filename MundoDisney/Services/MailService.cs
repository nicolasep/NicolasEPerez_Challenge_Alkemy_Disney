using MundoDisney.Entities;
using MundoDisney.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Services
{
    public class MailService : IMailService
    {
        private readonly ISendGridClient _client;
        public MailService(ISendGridClient client)
        {
            _client = client;
        }

        public async Task SendMail(Usuario user)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("nicolaseduardoperez@gmail.com", "Mundo Disney"),
                Subject = "Bienvenido a Mundo Disney",
                PlainTextContent = $"se ha registrado satistactoriamente su usuario {user.UserName}",
                HtmlContent = $"se ha registrado satistactoriamente su usuario {user.UserName}"
            };
            msg.AddTo(new EmailAddress(user.Email, "Test User"));

            await _client.SendEmailAsync(msg);    
        }
    }
}
