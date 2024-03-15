using System.ComponentModel.DataAnnotations;

namespace WebCol.Models
{
    public class ProductoPrincipio
    {
        public int ProductoPrincipioId { get; set; }
        public int ProductoId { get; set; }
        public int PrincipioId { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Principio Principio { get; set; }
    }
}
