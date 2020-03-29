using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppTeste.Models
{
    [Table("Bancos")]
    public class Banco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int    IdBanco   { get; set; }
        public string DescBanco { get; set; }


        public Banco() { }

        public Banco (int id)
        {
            this.IdBanco = id;
        }
        
        public Banco(string banco)
        {
            this.DescBanco = banco;
        }

        public Banco(int id, string banco)
        {
            this.IdBanco   = id;
            this.DescBanco = banco;
        }
    }
}