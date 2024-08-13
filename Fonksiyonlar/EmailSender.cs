using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using VeterinerApp.Fonksiyonlar.MailGonderme;

namespace VeterinerApp.Fonksiyonlar
{
    public class EmailSender : IEmailSender
    {
        private readonly string _smtpServer = "smtp-mail.outlook.com"; // SMTP sunucu adresi
        private readonly int _smtpPort = 587; // SMTP portu
        private readonly string _smtpUser = "umutdotnet@hotmail.com"; // SMTP kullanıcı adı
        private readonly string _smtpPass = "1989312caN."; // SMTP şifresi

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUser),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Timeout = 10000; // Zaman aşımı

                await client.SendMailAsync(mailMessage);
            }


        }
    }
}
