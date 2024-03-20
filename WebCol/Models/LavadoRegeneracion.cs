namespace WebCol.Models
{
    public class LavadoRegeneracion
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public required string ColumnaId { get; set; }
        public Columna Columna { get; set; }        
        public int AnalisisId { get; set; }
        public Analisis? Analisis { get; set; }
    }
}
