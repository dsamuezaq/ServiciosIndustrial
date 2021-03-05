using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DaemonIndustialMolinera.Base.Tablas
{
    [Table("MaestroProducto", Schema = "IndustrialMolinera")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string IdArticulo { get; set; }
        public string Descripcion { get; set; }

        public int Id_Línea { get; set; }
        public string Línea { get; set; }
        public int Id_Categoría { get; set; }
        public string Categoría { get; set; }
        public int Id_Marca { get; set; }
        public string Marca { get; set; }

        public string IdRubro { get; set; }
        public decimal? Iva { get; set; }
        public decimal? ImpuestosInternos { get; set; }
        public int? Exento { get; set; }
        public decimal? Precio1 { get; set; }
        public decimal? Precio2 { get; set; }
        public decimal? Precio3 { get; set; } = 0;
        public decimal? Precio4 { get; set; } = 0;
        public decimal? Precio5 { get; set; } = 0;
        public decimal? Precio6 { get; set; } = 0;
        public decimal? Precio7 { get; set; } = 0;
        public decimal? Precio8 { get; set; } = 0;
        public decimal? Precio9 { get; set; } = 0;
        public decimal? Precio10 { get; set; } = 0;
        public int? Idaccount { get; set; } = 0;
        public string StatusRegister { get; set; } = "A";
    }
}
