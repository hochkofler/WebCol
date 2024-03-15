using System.ComponentModel.DataAnnotations;
using WebCol.Utilities;

namespace WebCol.Models
{
    public class Analisis
    {
        public int Id { get; set; }

        [Display(Name = "Columna")]
        public String ColumnaId { get; set; }

        [DataType(DataType.Date)]
        [DateNoFuture]
        [Comparison(nameof(FechaFinal), ComparisonType.GreaterThanOrEqualTo)]
        [Display(Name = "Fecha inicio")]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [DateNoFuture]
        [Comparison(nameof(FechaInicio), ComparisonType.GreaterThanOrEqualTo)]
        [Display(Name = "Fecha final")]
        public DateTime FechaFinal { get; set; }

        public int CategoriaOrigen { get; set; }

        public string LoteId { get; set; }

        public List<Principio>? PrincipiosActivos { get; set; }

        [Display(Name = "pH")]
        [Range(0, 14)]
        public decimal Ph { get; set; }

        [Display(Name = "Tiempo de corrida [HH:MM]")]
        public TimeOnly TiempoCorrida { get; set; }

        [Display(Name = "Flujo [mL/min]")]
        public decimal Flujo { get; set; }

        [Display(Name = "Temp. [°C]")]
        public decimal Temperatura { get; set; }

        [Display(Name = "Presión inicial [MPa]")]
        public decimal PresionIni { get; set; }

        [Display(Name = "Presión final [Mpa]")]
        public decimal PresionFin { get; set; }

        [Display(Name = "Platos inicial")]
        public int PlatosIni { get; set; }

        [Display(Name = "Platos final")]
        public int PlatosFin { get; set; }

        [Display(Name = "Comportamiento")]
        public string Comportamiento { get; set; }

        public string? Comentario { get; set; }

        public Lote Lote { get; set; }

        public Columna? Columnas { get; set; }
    }
}
