using System.ComponentModel.DataAnnotations;
using WebCol.Utilities;

namespace WebCol.Models
{
    public class Columna
    {
        public required string Id { get; set; }

        [DataType(DataType.Date)]
        [DateNoFuture]
        [Comparison(nameof(FechaEnMarcha), ComparisonType.LessThanOrEqualTo)]
        [Display(Name = "Fecha ingreso")]
        public DateTime FechaIngreso { get; set; }

        [DataType(DataType.Date)]
        [DateNoFuture]
        [Comparison(nameof(FechaIngreso), ComparisonType.GreaterThanOrEqualTo)]
        [Display(Name = "Fecha marcha")]
        public DateTime FechaEnMarcha { get; set; }
        [Display(Name = "Dimensiones")]
        public required string Dimension { get; set; }
        [Display(Name = "Fase estacionaria")]
        public required string FaseEstacionaria { get; set; }
        [Display(Name = "Clase")]
        public required string Clase { get; set; }

        [Display(Name = "pH Mínimo")]
        [Comparison(nameof(PhMax), ComparisonType.LessThanOrEqualTo)] // Se corrigió la comparación
        [Range(0, 14)]
        public decimal PhMin { get; set; }

        [Display(Name = "pH Maximo")]
        [Comparison(nameof(PhMin), ComparisonType.GreaterThanOrEqualTo)] // Se corrigió la comparación
        [Range(0, 14)]
        public decimal PhMax { get; set; }
        [Range(0, 2000)]
        [Display(Name = "Presión Maxima")]
        public decimal PresionMax { get; set; }
        [Display(Name = "Modelo")]
        public required int MarcaId { get; set; }
        public Modelo? Marca { get; set; }
        public List<FaseMovil>? FasesMoviles { get; set; }
        public ICollection<Analisis> Analisis { get; set; } = new List<Analisis>();
    }
}
