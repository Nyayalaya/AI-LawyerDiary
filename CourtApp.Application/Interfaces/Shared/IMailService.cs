using CourtApp.Application.DTOs.Mail;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Shared
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}