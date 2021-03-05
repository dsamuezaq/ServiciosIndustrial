using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DaemonIndustialMolinera.baseChariot
{
    [Table("visitasUio", Schema = "MardisOrders")]
  public  class VisitaUIO
    {
        [Key]
        public int id { get; set; }
        public int? migrado { get; set; }
        
    }
}
