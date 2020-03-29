using System;
using System.Collections.Generic;
using System.Text;

namespace Teste03.Models
{
    public class Orcamento
    {
        public int       IdOrcamento    { get; set; }
                         
        public int       IdColeta       { get; set; }
        public int       IdCliente      { get; set; }
        public int       IdMotorista    { get; set; }
        public string    Valor          { get; set; }
        public DateTime  DataCadastro   { get; set; }
        public int       IdStatus       { get; set; }
        public DateTime? DataAceite     { get; set; }
        public DateTime? DataRecusa     { get; set; }
        public string    Observacoes    { get; set; }
        public int?      Visualiza      { get; set; }
        public int       IdVeiculoUsado { get; set; }
        public string    DescStatus     { get; set; }

        public Orcamento() { }

        public Orcamento(int idOrcamento)
        {
            this.IdOrcamento = idOrcamento;
        }

        public Orcamento (int idOrcamento, int idStatus)
        {
            this.IdOrcamento = idOrcamento;
            this.IdStatus    = idStatus;
        }

        public Orcamento( int IdColeta, int IdCliente, int IdMotorista, string Valor, DateTime DataCadastro, 
                          int IdStatus, DateTime DataAceite, DateTime DataRecusa, string Observacoes, int Visualiza,
                          int IdVeiculoUsado, string DescStatus)
        {
            this.IdColeta       = IdColeta;
            this.IdCliente      = IdCliente;
            this.IdMotorista    = IdMotorista;
            this.Valor          = Valor;
            this.DataCadastro   = DataCadastro;
            this.IdStatus       = IdStatus;
            this.DataAceite     = DataAceite;
            this.DataRecusa     = DataRecusa;
            this.Observacoes    = Observacoes;
            this.Visualiza      = Visualiza;
            this.IdVeiculoUsado = IdVeiculoUsado;
            this.DescStatus     = DescStatus;
        }

        public Orcamento( int IdCliente, int IdMotorista, string Valor, DateTime DataCadastro, 
                          int IdStatus, DateTime DataAceite, DateTime DataRecusa, string Observacoes,int Visualiza,
                          int IdVeiculoUsado, string DescStatus)
        {
            this.IdCliente      = IdCliente;
            this.IdMotorista    = IdMotorista;
            this.Valor          = Valor;
            this.DataCadastro   = DataCadastro;
            this.IdStatus       = IdStatus;
            this.DataAceite     = DataAceite;
            this.DataRecusa     = DataRecusa;
            this.Observacoes    = Observacoes;
            this.Visualiza      = Visualiza;
            this.IdVeiculoUsado = IdVeiculoUsado;
            this.DescStatus     = DescStatus;
        }

    }
}
