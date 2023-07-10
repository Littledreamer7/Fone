using FoneApi.Model;

namespace FoneApi.Service.Interface
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
