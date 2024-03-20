namespace WebCol.Models
{
    public class Comportamiento
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public ICollection<Analisis>? Analisis { get; set; } = new List<Analisis>();

    }
}
