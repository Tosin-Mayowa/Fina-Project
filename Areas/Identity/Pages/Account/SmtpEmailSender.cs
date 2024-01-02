using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace WebApplication2.Areas.Identity.Pages.Account
{


    public class SmtpEmailSender : IEmailSender
    {

        private readonly IConfiguration _configuration;

        public SmtpEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var smtpUsername = _configuration["smtpUsername"];
                var smtpServer = _configuration["smtpServer"];
                var smtpPassword = _configuration["smtpPassword"];
                var smtpPortString = _configuration["smtpPort"];
                var smtpPort = Int32.Parse(smtpPortString);

                MailMessage message = new MailMessage();
                // From ur smtp user name which is ur email address
                message.From = new MailAddress(smtpUsername);

                message.To.Add(new MailAddress(email));
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = htmlMessage;
                // smtp server and port for your smtp server goes here 
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    //smtp username and password here
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.Timeout = 10000;
                    await smtpClient.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }



    //public class SmtpEmailSender : IEmailSender
    //{

    //    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    //    {
    //        try
    //        {

    //            MailMessage message = new MailMessage();

    //            message.From = new MailAddress("toss800@gmail.com");
    //            message.To.Add(new MailAddress(email));
    //            message.Subject = subject;
    //            message.Body = htmlMessage;
    //            message.IsBodyHtml = true;

    //            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com",465);
    //            smtpClient.EnableSsl = true;
    //            smtpClient.UseDefaultCredentials = false;
    //            smtpClient.Credentials = new NetworkCredential("toss800@gmail.com", "mkke eqkx kxuw lexm");
    //            //smtpClient.Credentials = new NetworkCredential("regixapp@zohomail.com", "Mayowa1@");
    //            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
    //            await  smtpClient.SendMailAsync(message);

    //        }catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.ToString());
    //        }




    //    }
    //}
}
