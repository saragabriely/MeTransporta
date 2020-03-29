using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppTeste.Models
{
    [Table("Status")]
    public class Status
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int    IdStatus          { get; set; }

        public string DescricaoStatus   { get; set; } 

        public Status() { }

        public Status (int id)
        {
            this.IdStatus = id;
        }

        public Status (string status)
        {
            this.DescricaoStatus = status;
        }

        public Status (int id, string status)
        {
            this.IdStatus        = id;
            this.DescricaoStatus = status;
        }
    }
}