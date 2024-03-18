namespace WebCol.Models
{
    public class AnalisisProductoPrincipio
    {
        public int AnalisisId { get; set; }
        public Analisis Analisis { get; set; }

        public int ProductoId { get; set; }
        public int PrincipioId { get; set; }
        public ProductoPrincipio ProductoPrincipio { get; set; }
    }
}
