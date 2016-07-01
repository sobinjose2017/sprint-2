using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace myservice
{
    public class emailsend
    {
        public MailAddress to { get; set; }
        public MailAddress from { get; set; }
        public string sub { get; set; }
        public string body { get; set; }
        public string ToAdmin()
        {
            string feedback = "";
            emailsend me = new emailsend();

            var m = new MailMessage()
            {

                Subject = sub,
                Body = body,
                IsBodyHtml = true
            };
            to = new MailAddress("21241112@dut4life.ac.za", "Administrator");
            m.To.Add(to);
            m.From = new MailAddress(from.ToString());
            m.Sender = to;


            SmtpClient smtp = new SmtpClient
            {
                Host = "pod51014.outlook.com",
                Port = 587,
                Credentials = new NetworkCredential("sobin@baabte.com", "sobin001?"),
                EnableSsl = true
            };

            try
            {
                smtp.Send(m);
                feedback = "Message sent to insurance";
            }
            catch (Exception e)
            {
                feedback = "Message not sent retry" + e.Message;
            }
            return feedback;
        }
    }
}