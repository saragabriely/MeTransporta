using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppTeste.Models
{
    [Table("Motorista")]
    public class Motorista
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int      IdMotorista    { get; set; }

        public int      IdCliente      { get; set; }
        public string   MnumeroCnh     { get; set; }
        public string   McategoriaCnh  { get; set; }
        public string   MvalidadeCnh   { get; set; }
        public int      IdStatus       { get; set; }
        public string   Ccpf           { get; set; }

        public Motorista() { }

        public Motorista (int idMotorista)
        {
            this.IdMotorista = idMotorista;
        }

        public Motorista (string cpf)
        {
            this.Ccpf = cpf;
        }

        public Motorista(int IdMotorista, int IdCliente, string MnumeroCnh, string McategoriaCnh,
                         string MvalidadeCnh, int IdStatus,  string Ccpf )
        {
            this.IdMotorista   = IdMotorista;
            this.IdCliente     = IdCliente;
            this.MnumeroCnh    = MnumeroCnh;
            this.McategoriaCnh = McategoriaCnh;
            this.MvalidadeCnh  = MvalidadeCnh;
            this.IdStatus      = IdStatus;
            this.Ccpf          = Ccpf;
        }

        public Motorista(int IdCliente, string MnumeroCnh, string McategoriaCnh,
                         string MvalidadeCnh, int IdStatus,  string Ccpf )
        {
            this.IdCliente     = IdCliente;
            this.MnumeroCnh    = MnumeroCnh;
            this.McategoriaCnh = McategoriaCnh;
            this.MvalidadeCnh  = MvalidadeCnh;
            this.IdStatus      = IdStatus;
            this.Ccpf          = Ccpf;
        }

        
    }
}