using bmbox_main.Models.Utils;
using System;
using System.Net;
using System.Net.Mail;

namespace bmbox_main.Controllers.Encapsulations
{
    public class EmailHandler
    {
        private const string SENDER = "wad.2016-2017@yandex.ru";
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
            try
            {
                var emailMsg = new MailMessage(SENDER, receiver, SUBJECT + SENDER_DETAILS, WELCOME_MESSAGE);
                var smtpClient = new SmtpClient(SENDER_SERVER, PORT);
                smtpClient.Credentials = new NetworkCredential(NETWORK_CREDS_1, NETWORK_CREDS_2);
                smtpClient.EnableSsl = true;
                smtpClient.Send(emailMsg);
                return Constants.SUCCESS;
            }
            catch (Exception)
            {
                return Constants.FAIL;
            }

        }
    }
}