using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DaemonIndustialMolinera.Base.Tablas
{
    [Table("DevolucionFactura", Schema = "MardisOrders")]
   public class Devolucion
    {
        [Key]
        public int Id { get; set; }
        public double d_ESTADO { get; set; }
    }
}
