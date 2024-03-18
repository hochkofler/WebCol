using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCol.Models
{
    public class ProductoPrincipio
    {
        public int ProductoId { get; set; }
        public int PrincipioId { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Principio Principio { get; set; }
        public List<Analisis>? Analisis { get; set; }

    }
}
