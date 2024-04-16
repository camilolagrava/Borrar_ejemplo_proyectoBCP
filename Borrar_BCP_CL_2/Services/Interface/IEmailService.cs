using Borrar_BCP_CL_2.Models.Dto;

namespace Borrar_BCP_CL_2.Services.Interface
{
    public interface IEmailService
    {
        void SendEmail(EmailDTO request);
    }
}
