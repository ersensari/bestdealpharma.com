using System.Threading.Tasks;

namespace bestdealpharma.com.Helpers
{
  public interface IEmailService
  {
    Task SendEmail(string email, string subject, string message);
  }
}
