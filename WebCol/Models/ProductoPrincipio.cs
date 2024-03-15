using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCol.Models
{
    public class ProductoPrincipio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoPrincipioId { get; set; }
        public int ProductoId { get; set; }
        public int PrincipioId { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Principio Principio { get; set; }
    }
}
