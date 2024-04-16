namespace Borrar_BCP_CL_2.Models.ost
{
    public partial class Respuesta
    {
        public string ContratoID { get; set; }
        public DateOnly? Fecha { get; set; }

        public string? RepresentanteLegal { get; set; }

        public string? NombreProvedor { get; set; }

        public string? DocumentoProvedor { get; set; }

        public string? Domicilio { get; set; }

        public string? Direccion { get; set; }

        public string? Ciudad { get; set; }

        public int? Superficie { get; set; }

        public decimal? Importe { get; set; }

        public string? Cuenta { get; set; }

    }
}
