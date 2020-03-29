using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppTeste.Models
{
    [Table("BandeiraCartao")]
    public partial class BandeiraCartao
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int    IdBandeira { get; set; }

        public int    Digito     { get; set; }

        public string Descricao  { get; set; }

        public BandeiraCartao() { }

        public BandeiraCartao(int idBandeira)
        {
            this.IdBandeira = idBandeira;
        }

        public BandeiraCartao(int idBandeira, string descricao)
        {
            this.IdBandeira = idBandeira;
            this.Descricao = descricao;
        }

        public BandeiraCartao(string descricao)
        {
            this.Descricao = descricao;
        }
    }
}