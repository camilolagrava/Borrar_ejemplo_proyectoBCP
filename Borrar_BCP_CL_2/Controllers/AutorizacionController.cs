using Borrar_BCP_CL_2.Models.ost;
using Borrar_BCP_CL_2.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Borrar_BCP_CL_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizacionController : ControllerBase
    {
        private readonly IAutorizationService _autorizationService;

        public AutorizacionController(IAutorizationService autorizationService)
        {
            _autorizationService = autorizationService;
        }

       /* [HttpPost]
        [Route("Autenticar")]
        public async Task<IActionResult> Autenticar(Login formulario)
        {
            var resultado = await _autorizationService.DE
        }*/

    }
}



