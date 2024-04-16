using System;
using System.Collections.Generic;

namespace Borrar_BCP_CL_2.Models;

public partial class Contrato
{
    public int? ContratoId { get; set; }

    public string? CodigoContrato { get; set; }

    public int? ClienteId { get; set; }

    public string? Testimonio { get; set; }

    public DateOnly? FechaInicial { get; set; }

    public DateOnly? FechaFinal { get; set; }

    public DateOnly? FechaTestimonio { get; set; }

    public int? NumeroNotaria { get; set; }

    public string? PaternoProvedor { get; set; }

    public string? MaternoProvedor { get; set; }

    public string? NombresProvedor { get; set; }

    public string? DocumentoProvedor { get; set; }

    public string? Domicilio { get; set; }

    public string? DireccionAmbiente { get; set; }

    public string? Ciudad { get; set; }

    public int? Superficie { get; set; }

    public string? NumeroDireccion { get; set; }

    public decimal? Importe { get; set; }

    public string? Literal { get; set; }

    public string? Cuenta { get; set; }

    public int? Meses { get; set; }

    public DateOnly? FechaInicialArrendamiento { get; set; }

    public DateOnly? FechaFinalArrendamiento { get; set; }

    public DateOnly? FechaTenor { get; set; }

    public string? Mes { get; set; }

    public string? Anno { get; set; }

    public virtual Cliente? Cliente { get; set; }
}
