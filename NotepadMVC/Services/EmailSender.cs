using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NotepadMVC.Services
{   //İf you wanna use direct gmail account 
    //https://www.google.com/settings/security/lesssecureapps use this address and remove the privacy rule
    public class EmailSender :IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string sender = "*****@gmail.com"; //need to write your email
            MailMessage mail = new MailMessage(sender, email, subject, htmlMessage);
            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("*******@gmail.com", "need to write your password");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }
    }
}
