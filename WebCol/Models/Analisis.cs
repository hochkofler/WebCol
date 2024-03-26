using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebCol.Utilities;

namespace WebCol.Models
{
    public class Analisis
    {
        public int Id { get; set; }

        [Display(Name = "Columna")]
        public int AsignacionColumnaId { get; set; }

        [DataType(DataType.Date)]
        [DateNoFuture]
        [Comparison(nameof(FechaFinal), ComparisonType.LessThanOrEqualTo)]
        [Display(Name = "Fecha inicio")]
        public DateTime FechaInicio { get; set; }


        [DataType(DataType.Date)]
        [DateNoFuture]
        [Comparison(nameof(FechaInicio), ComparisonType.GreaterThanOrEqualTo)]
        [Display(Name = "Fecha final")]
        public DateTime FechaFinal { get; set; }

        public int CategoriaOrigen { get; set; }

        public required string LoteId { get; set; }

        [Display(Name = "pH")]
        [Range(0, 14)]
        public decimal Ph { get; set; }

        [Display(Name = "Tiempo de corrida [min]")]
        public decimal TiempoCorrida { get; set; }

        [Display(Name = "Número de inyecciones")]
        public int Inyecciones { get; set; }

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

        [Display(Name = "Comentario")]
        public string? Comentario { get; set; }

        public Lote? Lote { get; set; } 
        public virtual AsignacionColumna? AsignacionColumnas { get; set; }

        public ICollection<LavadoRegeneracion>? LavadoRegeneracion { get; set; } = new List<LavadoRegeneracion>();
        public ICollection<Comportamiento> Comportamientos { get; set; } = new List<Comportamiento>();
        public List<int>? PrincipiosIds { get; set; }
                
        public string Usuario { get; set; }

    }
}
