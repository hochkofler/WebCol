namespace WebCol.Models
{
    public class Principio
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        public ICollection<ProductoPrincipio> ProductosPrincipios { get; set; } = [];
    }
}
