namespace WebCol.Models
{
    public class FaseMovil
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public ICollection<Columna>? Columnas { get; set; }
    }
}
