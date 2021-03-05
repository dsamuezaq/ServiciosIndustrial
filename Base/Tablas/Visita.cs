using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DaemonIndustialMolinera.Base.Tablas
{
    [Table("Visita", Schema = "IndustrialMolinera")]
   public class Visita
    {
        [Key]
        public int id { get; set; }
        public string codcliente { get; set; }
        public string codvendedor { get; set; }
        public DateTime fechavisita { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Linkfotoexterior { get; set; }
        public string Compro { get; set; }
        public string Observacion { get; set; }
        public string estado { get; set; }

    }
}
