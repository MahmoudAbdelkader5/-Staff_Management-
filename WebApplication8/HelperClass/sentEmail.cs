using data_Access_layer.model;
using System.Net;
using System.Net.Mail;

namespace WebApplication8.HelperClass
{
    public class sentEmail
    {
        public static void sendEmail(email e)
        {
            // code to send email
            if (e != null)
            {
                var client = new SmtpClient("smtp@gmail.com", 587);
                client.Credentials = new NetworkCredential("mm4084@fayoum.edu.eg", "mahmoud5719999");
                client.EnableSsl = true;
                client.Send("mm4084@fayoum.edu.eg", e.to, e.subject, e.body);

                // send email

            }
        }
    }
}
