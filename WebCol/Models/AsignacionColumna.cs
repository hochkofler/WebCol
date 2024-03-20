using Microsoft.EntityFrameworkCore;

namespace WebCol.Models
{
    public class AsignacionColumna
    {
        public int AsignacionColumnaId { get; set; }
        public bool Activa { get; set; }
        public required string ColumnaId { get; set; }
        public Columna Columnas { get; set; }
        public int ProcedimientoAnalisisId { get; set; }
        public ProcedimientoAnalisis ProcedimientoAnalisis { get; set; }
        public ICollection<Analisis>? Analisis { get; set; } = new List<Analisis>();
    }
}
