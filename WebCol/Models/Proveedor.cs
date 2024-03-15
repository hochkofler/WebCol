namespace WebCol.Models
{
    public class Proveedor
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public ICollection<Modelo> Marcas { get; } = new List<Modelo>();
    }
}
