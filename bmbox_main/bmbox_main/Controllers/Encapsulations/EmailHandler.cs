using bmbox_main.Models.Utils;
using System;
using System.Net;
using System.Net.Mail;

namespace bmbox_main.Controllers.Encapsulations
{
    public class EmailHandler
    {
        private const string SENDER = "bmbox.app@gmail.com";
        private const string SENDER_SERVER = "smtp.yandex.ru";
        private const string NETWORK_CREDS_1 = "wad.2016-2017";
        private const string NETWORK_CREDS_2 = "wad.2016-2017wiut";
        private const string SENDER_DETAILS = " from Bmbox Noreply";
        private const string SUBJECT = "Welcome message";
        private const int    PORT = 25;
        private const string WELCOME_MESSAGE = "Welcome to bmbox.com!\n Your registration has been completed successfuly.";

        public string receiver { get; set; }

        public EmailHandler(string receiver)
        {
            this.receiver = receiver;
        }

       
        public string SendEmail()
        {
            var pass = "bmbox_2018";
            try
            {
                using (MailMessage mm = new MailMessage(SENDER, receiver))
                {
                    mm.Subject = SUBJECT + SENDER_DETAILS;
                    mm.Body = WELCOME_MESSAGE;

                    mm.IsBodyHtml = false;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(SENDER, pass);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        return Constants.SUCCESS;
                    }
                }
            }
            catch (Exception e)
            {
                return Constants.FAIL;
            }
        }
    }
}