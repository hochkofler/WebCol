﻿namespace WebCol.Models.ViewModels
{
    public class AnalisisViewModel
    {
        public Analisis? Analisis { get; set; }
        public Lote? Lote { get; set; }
        public List<Columna>? Columnas { get; set; }
        public List<ProcedimientoAnalisis>? Procedimientos { get; set; }
        public List<AsignacionColumna>? AsignacionColumnas { get; set; }
    }
}
