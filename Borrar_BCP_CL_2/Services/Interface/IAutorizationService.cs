using Borrar_BCP_CL_2.Models;
using Borrar_BCP_CL_2.Models.Dto;
using Borrar_BCP_CL_2.Models.ost;

namespace Borrar_BCP_CL_2.Services.Interface
{
    public interface IAutorizationService
    {

        Task<AuthorizationResponse> GiveToken(Usuario usuario);
    }
}
