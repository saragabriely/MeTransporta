using System;
using System.Collections.Generic;
using System.Text;


namespace Teste03.Models
{
    public class Teste
    {
        public int    Id        { get; set; }
        public string Descricao { get; set; }

        public Teste() { }

        public Teste(int id, string descricao)
        {
            this.Id        = id;
            this.Descricao = descricao;
        }
    }
}
