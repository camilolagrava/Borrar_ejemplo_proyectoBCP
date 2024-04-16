using System;
using System.Collections.Generic;

namespace Borrar_BCP_CL_2.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string? Paterno { get; set; }

    public string? Materno { get; set; }

    public string? Nombres { get; set; }

    //public virtual ICollection<Contrato>? Contratos { get; set; } 
}
