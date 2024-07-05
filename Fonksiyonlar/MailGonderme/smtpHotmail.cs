using System.Net.Mail;

namespace VeterinerApp.Fonksiyonlar.MailGonderme
{
    public class smtpHotmail : SmtpClient
    {
        public smtpHotmail() : base("smtp-mail.outlook.com", 587)
        {

        }
    }
}
