using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DaemonIndustialMolinera.baseChariot
{
    [Table("Pedidos", Schema = "MardisOrders")]
    public class Order
    {
        public Order()
        {
          //  this.pedidosItems = new HashSet<OrderDetail>();
        }
        [Key]
        public int id { get; set; }
        public string codCliente { get; set; }
        public string fecha { get; set; }
        public string idVendedor { get; set; }
        public Nullable<decimal> totalNeto { get; set; }
        public Nullable<decimal> totalFinal { get; set; }
        public Nullable<decimal> transferido { get; set; }
        public Nullable<decimal> gpsX { get; set; }
        public Nullable<decimal> gpsY { get; set; }
        //public virtual ICollection<OrderDetail> pedidosItems { get; set; }
    }
}
