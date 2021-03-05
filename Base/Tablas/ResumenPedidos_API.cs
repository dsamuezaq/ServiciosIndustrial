using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DaemonIndustialMolinera.Base.Tablas
{
    [Table("ResumenPedidos_API", Schema = "IndustrialMolinera")]
    public class ResumenPedidos_API
    {

        [Key]
        public int IdPedidoDetalle { get; set; }
        public int id { get; set; }
        public string codCliente { get; set; }
        public string fecha { get; set; }
        public string idVendedor { get; set; }
        public decimal totalNeto { get; set; }
        public decimal totalFinal { get; set; }
        public string idArticulo { get; set; }
        public decimal importeUnitario { get; set; }
        public decimal cantidad { get; set; }
        public decimal total { get; set; }
        public string formapago { get; set; }
        public string unidad { get; set; }
        public DateTime FechaPedido { get; set; }
        public string p_PEDIDO_MARDIS { get; set; }
        
    }

}
