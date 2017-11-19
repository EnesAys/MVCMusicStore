using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace MVCMusicStore.Tools
{
    public class MailHelper
    {
        public static bool SendConfirmationMail(string userName, string password, string mail, string confirmationToken)
        {
            bool result = false;
            MailMessage msg = new MailMessage();
            msg.To.Add(mail);
            msg.Subject = "Welcome to our site";
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body><p>Welcome <b>{0}</b></p><p>Your password: <b>{1}</b></p><p>To activate your account please click <a href='http://localhost:1464/Account/Activation?confirmationToken={2}'>this</a> link.</p></body></html>", userName, password, confirmationToken);

            msg.From = new MailAddress("info@musicstore.com");//Mail Geçerli olmadığı için databaseden IsConfirmed true olmalı registration kısmında
            SmtpClient smtp = new SmtpClient("smtp.live.com", 587);
            smtp.EnableSsl = true;
            NetworkCredential cred = new NetworkCredential("aspuygulama@outlook.com", "denemehesabi1");
            smtp.Credentials = cred;
            try
            {
                smtp.Send(msg);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}