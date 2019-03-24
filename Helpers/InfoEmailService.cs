using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace bestdealpharma.com.Helpers
{
  public class InfoEmailService : IEmailService
  {
    private readonly IConfiguration _configuration;

    public InfoEmailService(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async Task SendEmail(string email, string subject, string message)
    {
      using (var client = new SmtpClient())
      {
        var credential = new NetworkCredential
        {
          UserName = _configuration["InfoEmail:Email"],
          Password = _configuration["InfoEmail:Password"]
        };

        client.Credentials = credential;
        client.Host = _configuration["InfoEmail:Host"];
        client.Port = int.Parse(_configuration["InfoEmail:Port"]);
        client.EnableSsl = true;
        client.Timeout = 10000;
        using (var emailMessage = new MailMessage())
        {
          emailMessage.To.Add(new MailAddress(email));
          emailMessage.From = new MailAddress(_configuration["InfoEmail:Email"]);
          emailMessage.Subject = subject;
          emailMessage.Body = message;
          emailMessage.IsBodyHtml = true;
          try
          {
            client.Send(emailMessage);
          }
          catch (Exception e)
          {
            Console.WriteLine(e);
            throw e;
          }
        }
      }

      await Task.CompletedTask;
    }
  }
}
