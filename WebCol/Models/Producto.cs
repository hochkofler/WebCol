namespace WebCol.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Tipo { get; set; }
        public required string Registro { get; set; }

        public List<ProductoPrincipio> ProductosPrincipios { get; set; } = [];
        public List<Principio> Principios { get; set; } = [];
        public ICollection<Lote> Lotes { get; set; } = new List<Lote>();
    }
}
