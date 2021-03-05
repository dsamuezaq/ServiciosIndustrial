using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DaemonIndustialMolinera.Base.Tablas
{
    [Table("ClienteNuevos", Schema = "IndustrialMolinera")]
    public class ClienteNuevo
    {
        [Key]
        public int ID { get; set; }
        
        public int CodigoCliente { get; set; }
        public string Code { get; set; }
        public string Documento { get; set; }

        public string nombreLocal { get; set; }
        public string Direcion { get; set; }
        public string negocio { get; set; }
        public int dia { get; set; }
        public string ESTADO { get; set; }
        public string ruta { get; set; }

        public string cov { get; set; }
        public string vendedor { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string CodigoMardis { get; set; }
    }
}
