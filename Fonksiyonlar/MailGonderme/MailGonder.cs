using System;
using System.Net;
using System.Net.Mail;

namespace VeterinerApp.Fonksiyonlar.MailGonderme
{
    public class MailGonder : MailMessage
    {
        public string senderMail { get; set; }
        public string senderPassword { get; set; }
        public string receiverMail { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

        public MailGonder(string mailAdres, string body, string baslik)
        {
            this.senderMail = "umutdotnet@hotmail.com";
            this.senderPassword = "1989312caN.";
            this.receiverMail = mailAdres;
            this.body = body;


        }
        public bool MailGonderHotmail(MailGonder mail)
        {
            smtpHotmail smtpHotmail = new smtpHotmail();
            smtpHotmail.UseDefaultCredentials = false;
            smtpHotmail.Credentials = new NetworkCredential(mail.senderMail, mail.senderPassword);
            smtpHotmail.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpHotmail.EnableSsl = true;

            try
            {
                smtpHotmail.Send(mail.senderMail, mail.receiverMail, mail.subject, mail.body);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
