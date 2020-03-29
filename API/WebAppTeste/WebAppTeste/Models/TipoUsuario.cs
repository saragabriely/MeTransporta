using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppTeste.Models
{
    [Table("TipoUsuario")]
    public class TipoUsuario 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int    Id        { get; set; }

        public string Descricao { get; set; }

        public TipoUsuario() { }

        public TipoUsuario(int id)
        {
            this.Id = id;
        }

        public TipoUsuario(int id, string descricao)
        {
            this.Id = id;
            this.Descricao = descricao;
        }

        public TipoUsuario(string descricao)
        {
            this.Descricao = descricao;
        }

    }
}