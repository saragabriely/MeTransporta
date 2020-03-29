using System;
using System.Collections.Generic;
using System.Text;

namespace Teste03.Models
{
    public class ContaBancaria
    {
        public int      IdContaBancaria     { get; set; }

        public int?     IdCliente           { get; set; }
        public int?     IdBanco             { get; set; }
        public int      MAgencia            { get; set; }
        public int?     MDigAgencia         { get; set; }
        public int      MConta              { get; set; }
        public int      MDigConta           { get; set; }
        public int?     IdTipoConta         { get; set; }
        public DateTime MDataCadastro       { get; set; }
        public int      IdStatus            { get; set; }
        public string   Ccpf                { get; set; }
        public DateTime? MUltimaAtualizacao { get; set; }
        public string   BancoDesc           { get; set; }
        public string   TipoContaBanDesc    { get; set; }

        public ContaBancaria() { }

        public ContaBancaria(string cpf)
        {
            this.Ccpf = cpf;
        }

        public ContaBancaria(int id)
        {
            this.IdContaBancaria = id;
        }

        public ContaBancaria(string cpf, int idTipoConta)
        {
            this.Ccpf        = cpf;
            this.IdTipoConta = idTipoConta;
        }

        public ContaBancaria(int IdContaBancaria, int IdCliente, int IdBanco,  int MAgencia, int MDigAgencia,
                             int MConta,  int MDigConta,  int IdTipoConta, DateTime MDataCadastro, int IdStatus,
                             string Ccpf, DateTime MUltimaAtualizacao,  string BancoDesc, string TipoContaBanDesc)
        {
            this.IdContaBancaria    = IdContaBancaria;
            this.IdCliente          = IdCliente;
            this.IdBanco            = IdBanco;
            this.MAgencia           = MAgencia;
            this.MDigAgencia        = MDigAgencia;
            this.MConta             = MConta;
            this.MDigConta          = MDigConta;
            this.IdTipoConta        = IdTipoConta;
            this.MDataCadastro      = MDataCadastro;
            this.IdStatus           = IdStatus;
            this.Ccpf               = Ccpf;
            this.MUltimaAtualizacao = MUltimaAtualizacao;
            this.BancoDesc          = BancoDesc;
            this.TipoContaBanDesc   = TipoContaBanDesc;
        }

        public ContaBancaria(int IdCliente, int IdBanco,  int MAgencia, int MDigAgencia,
                             int MConta,  int MDigConta,  int IdTipoConta, DateTime MDataCadastro, int IdStatus,
                             string Ccpf, DateTime MUltimaAtualizacao,  string BancoDesc, string TipoContaBanDesc)
        {
            this.IdCliente          = IdCliente;
            this.IdBanco            = IdBanco;
            this.MAgencia           = MAgencia;
            this.MDigAgencia        = MDigAgencia;
            this.MConta             = MConta;
            this.MDigConta          = MDigConta;
            this.IdTipoConta        = IdTipoConta;
            this.MDataCadastro      = MDataCadastro;
            this.IdStatus           = IdStatus;
            this.Ccpf               = Ccpf;
            this.MUltimaAtualizacao = MUltimaAtualizacao;
            this.BancoDesc          = BancoDesc;
            this.TipoContaBanDesc   = TipoContaBanDesc;
        }


    }
}
