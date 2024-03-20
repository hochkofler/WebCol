namespace WebCol.Models
{
    public class ProcedimientoAnalisis
    {
        public int Id { get; set; }
        public int FaseMovilId { get; set; }
        public FaseMovil FaseMovil { get; set; }
        public List<AsignacionColumna> AsignacionesColumnas { get; set; } = [];
        public List<Columna> Columnas { get; set; } = [];
        public required string Tipo { get; set; }

        // Claves foráneas
        public int ProductoId { get; set; }
        public int PrincipioId { get; set; }

        // Propiedad de navegación
        public virtual ProductoPrincipio ProductoPrincipio { get; set; }
        public virtual Principio Principio { get; set; }
        public virtual Producto Producto { get; set; }

    }
}
