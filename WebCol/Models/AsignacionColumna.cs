using Microsoft.EntityFrameworkCore;

namespace WebCol.Models
{
    public class AsignacionColumna
    {
        public int AsignacionColumnaId { get; set; }
        public int ProductoId { get; set; } // Agregamos esta propiedad
        public int PrincipioId { get; set; } // Agregamos esta propiedad
        public string ColumnaId { get; set; }
        public required Columna Columna { get; set; }
        public virtual ProductoPrincipio ProductoPrincipio { get; set; }
    }
}
