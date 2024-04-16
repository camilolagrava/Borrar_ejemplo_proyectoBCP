using System;
using System.Collections.Generic;

namespace Borrar_BCP_CL_2.Models;

public partial class Usuario
{
    public int? UsuarioId { get; set; }

    public string Nombre { get; set; }

    public string Email { get; set; }

    public string Contrasenna { get; set; }
}
