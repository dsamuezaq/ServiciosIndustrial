using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DaemonIndustialMolinera.baseChariot
{
    [Table("Branch", Schema = "MardisCore")]
   public class Locales
    {
        [Key]
        public int Id { get; set; }
        public int IdAccount { get; set; }
        public string Code { get; set; }
        public string ExternalCode { get; set; }
        public string CommentBranch { get; set; }
    }
}
