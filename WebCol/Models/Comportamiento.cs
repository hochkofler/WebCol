namespace WebCol.Models
{
    public class Comportamiento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Analisis>? Analisis { get; set; } = [];

    }
}
