using Borrar_BCP_CL_2.Models.ost;

namespace Borrar_BCP_CL_2.Models.Dto
{
    public partial class Respuesta_Conteo
    {
        public virtual ICollection<Respuesta>? Respuestas { get; set; }

        public int? cuantity { get; set; }

        public virtual ICollection<int>? ClientesInexistentes { get; set; }
    }
}
