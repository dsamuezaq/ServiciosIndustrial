﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DaemonIndustialMolinera.baseChariot
{
    [Table("vw_DevolucionFactura", Schema = "dbo")]
    public class VistaDevolucion
    {
        [Key]
        public int Id { get; set; }
        public double d_DEVOLUCION { get; set; }
        public double d_ORDEN { get; set; }
        public string d_FECHA { get; set; }
        public double d_FACTURA { get; set; }
        public double d_CLIENTE { get; set; }
        public string d_PRODUCTO { get; set; }
        public double d_PRECIO { get; set; }
        public double d_CANTIDAD { get; set; }
        public double d_VENDEDOR { get; set; }
        public double d_ESTADO { get; set; }
        public string d_PEDIDO_MARDIS { get; set; }

    }
}
