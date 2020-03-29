using System;
using System.Collections.Generic;
using System.Text;

namespace Teste03.Models
{
    public class CartaoCredito
    {
        public int       IdCartao      { get; set; }

        public int?      IdCliente     { get; set; }
        public string    Ccpf          { get; set; }
        public string    CNumeroCartao { get; set; }
        public int?      IdBandeira    { get; set; }
        public string    CDataValidade { get; set; }
        public int       CCodigoSeg    { get; set; }
        public DateTime  CDataCadastro { get; set; }
        public int       IdStatus      { get; set; }
        
        public DateTime? CDataInativacao    { get; set; }
        
        public DateTime? CUltimaAtualizacao { get; set; }

        public string    BandeiraDescricao { get; set; }
        public string    NomeImpresso      { get; set; }

        public CartaoCredito() { }

        public CartaoCredito (int id)
        {
            this.IdCartao = id;
        }

        public CartaoCredito (string cpf)
        {
            this.Ccpf = cpf;
        }

        public CartaoCredito( int IdCartao,   int IdCliente,        string Ccpf,   string CNumeroCartao, 
                              int IdBandeira, string CDataValidade, int CCodigoSeg, DateTime CDataCadastro, int IdStatus,
                              DateTime CDataInativacao, DateTime CUltimaAtualizacao, string BandeiraDescricao,
                              string NomeImpresso )
        {
            this.IdCartao       = IdCartao;
            this.IdCliente      = IdCliente;
            this.Ccpf           = Ccpf;
            this.CNumeroCartao  = CNumeroCartao;
            this.IdBandeira     = IdBandeira;
            this.CDataValidade  = CDataValidade;
            this.CCodigoSeg     = CCodigoSeg;
            this.CDataCadastro  = CDataCadastro;
            this.IdStatus       = IdStatus;
            this.CDataInativacao    = CDataInativacao;
            this.CUltimaAtualizacao = CUltimaAtualizacao;
            this.BandeiraDescricao  = BandeiraDescricao;
            this.NomeImpresso   = NomeImpresso;
        }

        public CartaoCredito( int IdCliente,        string Ccpf,    string CNumeroCartao,  int IdBandeira, 
                              string CDataValidade, int CCodigoSeg, DateTime CDataCadastro, int IdStatus,
                              DateTime CDataInativacao, DateTime CUltimaAtualizacao, string BandeiraDescricao,
                              string NomeImpresso)
        {
            this.IdCliente      = IdCliente;
            this.Ccpf           = Ccpf;
            this.CNumeroCartao  = CNumeroCartao;
            this.IdBandeira     = IdBandeira;
            this.CDataValidade  = CDataValidade;
            this.CCodigoSeg     = CCodigoSeg;
            this.CDataCadastro  = CDataCadastro;
            this.IdStatus       = IdStatus;
            this.CDataInativacao    = CDataInativacao;
            this.CUltimaAtualizacao = CUltimaAtualizacao;
            this.BandeiraDescricao  = BandeiraDescricao;
            this.NomeImpresso   = NomeImpresso;
        }
    }
}
