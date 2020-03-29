using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTeste.Models
{
    [Table("TipoVeiculo")]
    public class TipoVeiculo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int    IdTipoVeiculo { get; set; }

        public string Tipo_Veiculo  { get; set; }

        public TipoVeiculo() { }

        public TipoVeiculo(int id)
        {
            this.IdTipoVeiculo = id;
        }

        public TipoVeiculo (int id, string descricao)
        {
            this.IdTipoVeiculo   = id;
            this.Tipo_Veiculo = descricao;
        }

        public TipoVeiculo(string descricao)
        {
            this.Tipo_Veiculo = descricao;
        }
    }
}