using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppTeste.Models
{
    [Table("teste")]
    public partial class teste
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int    IdTeste   { get; set; }
                
        public string descricao { get; set; }

        public teste() { }
        
        public teste(int idTeste)
        {
            this.IdTeste = idTeste;
        }
        
        public teste(int idTeste, string descricao)
        {
            this.IdTeste = idTeste;
            this.descricao = descricao;
        }

        public teste(string descricao)
        {
            this.descricao = descricao;
        }

       // public List<teste> lista = new List<teste>();


    }
}