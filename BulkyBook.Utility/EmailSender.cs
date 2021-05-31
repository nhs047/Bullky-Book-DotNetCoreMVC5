using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions _emailOptions;
        public EmailSender(IOptions<EmailOptions> options)
        {
            _emailOptions = options.Value;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage mm = new MailMessage(_emailOptions.SenderEmail, email);
            mm.Subject = subject;
            mm.Body = htmlMessage;
            mm.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            System.Net.NetworkCredential nc = new System.Net.NetworkCredential(_emailOptions.SenderEmail, _emailOptions.SenderSecret);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = nc;
            smtp.Send(mm);
            return Task.CompletedTask;
        }
    }
}
