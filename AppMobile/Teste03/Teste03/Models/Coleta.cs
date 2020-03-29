using System;
using System.Collections.Generic;
using System.Text;

namespace Teste03.Models
{
    public class Coleta
    {
        public int      IdColeta              { get; set; }
        
        public string   EndRetCep             { get; set; }
        public string   EndRetUf              { get; set; }
        public string   EndRetEndereco        { get; set; }
        public string   EndRetNumero          { get; set; }
        public string   EndRetComplemento     { get; set; }
        public string   EndRetBairro          { get; set; }
        public string   EndRetCidade          { get; set; }
        public string   EndRetNomeResponsavel { get; set; }
        public string   EndRetRespTelefone    { get; set; }
        public string   EndEntCep             { get; set; }
        public string   EndEntUf              { get; set; }
        public string   EndEntEndereco        { get; set; }
        public string   EndEntNumero          { get; set; }
        public string   EndEntComplemento     { get; set; }
        public string   EndEntBairro          { get; set; }
        public string   EndEntCidade          { get; set; }
        public string   EndEntNomeResponsavel { get; set; }
        public string   EndEntRespTelefone    { get; set; }
        public string   DataMaxima            { get; set; }
        public string    HorarioLimite        { get; set; }
        public string    ValorPretendido      { get; set; }
        public string    Observacoes          { get; set; }
        public string    ApelidoColeta        { get; set; }
        public int       IdStatus             { get; set; }
        public string    MatTipo              { get; set; }
        public string    MatFragilidade       { get; set; }
        public string    MatDescricao         { get; set; }
        public string    MatPeso              { get; set; }
        public string    MatVolume            { get; set; }
        public string    MatAltura            { get; set; }
        public string    MatLargura           { get; set; }
        public DateTime  DataCadastro         { get; set; }
        public DateTime? UltimaAtualizacao    { get; set; }
        public int       IdCliente            { get; set; }
        public string    HorarioLimite02      { get; set; }
        public string    TipoVeiculo          { get; set; }
        public string    DescricaoStatus      { get; set; }

        public Coleta() { }

        public Coleta (int idColeta)
        {
            this.IdColeta = idColeta;
        }

        public Coleta (int IdColeta, string EndRetCep, string EndRetUf, string EndRetEndereco, string EndRetNumero,
                       string EndRetComplemento,  string EndRetBairro, string EndRetCidade,string EndRetNomeResponsavel,
                       string EndRetRespTelefone, string EndEntCep,    string EndEntUf,    string EndEntEndereco, string EndEntNumero, 
                       string EndEntComplemento,  string EndEntBairro, string EndEntCidade,string EndEntNomeResponsavel, 
                       string EndEntRespTelefone, string DataMaxima, string HorarioLimite, string ValorPretendido, string Observacoes,
                       string   ApelidoColeta, int IdStatus, string MatTipo, string MatFragilidade, string MatDescricao, string MatPeso,
                       string MatVolume, string MatAltura, string MatLargura, DateTime DataCadastro, DateTime UltimaAtualizacao, 
                       int idCliente, string HorarioLimite02, string TipoVeiculo, string DescricaoStatus)
        {
            this.IdColeta              = IdColeta;
            this.EndRetCep             = EndRetCep;
            this.EndRetUf              = EndRetUf;
            this.EndRetEndereco        = EndRetEndereco;
            this.EndRetNumero          = EndRetNumero;
            this.EndRetComplemento     = EndRetComplemento;
            this.EndRetBairro          = EndRetBairro;
            this.EndRetCidade          = EndRetCidade;
            this.EndRetNomeResponsavel = EndRetNomeResponsavel;
            this.EndRetRespTelefone    = EndRetRespTelefone;
            this.EndEntCep             = EndEntCep;
            this.EndEntUf              = EndEntUf;
            this.EndEntEndereco        = EndEntEndereco;
            this.EndEntNumero          = EndEntNumero;
            this.EndEntComplemento     = EndEntComplemento;
            this.EndEntBairro          = EndEntBairro;
            this.EndEntCidade          = EndEntCidade;
            this.EndEntNomeResponsavel = EndEntNomeResponsavel;
            this.EndEntRespTelefone    = EndEntRespTelefone;
            this.DataMaxima            = DataMaxima;
            this.HorarioLimite         = HorarioLimite;
            this.ValorPretendido       = ValorPretendido;
            this.Observacoes           = Observacoes;
            this.ApelidoColeta         = ApelidoColeta;
            this.IdStatus              = IdStatus;
            this.MatTipo               = MatTipo;
            this.MatFragilidade        = MatFragilidade;
            this.MatDescricao          = MatDescricao;
            this.MatPeso               = MatPeso;
            this.MatVolume             = MatVolume;
            this.MatAltura             = MatAltura;
            this.MatLargura            = MatLargura;
            this.DataCadastro          = DataCadastro;
            this.UltimaAtualizacao     = UltimaAtualizacao;
            this.IdCliente             = idCliente;
            this.HorarioLimite02       = HorarioLimite02;
            this.TipoVeiculo           = TipoVeiculo;
            this.DescricaoStatus       = DescricaoStatus;
        }

        public Coleta ( string EndRetCep, string EndRetUf, string EndRetEndereco, string EndRetNumero,
                       string EndRetComplemento, string EndRetBairro, string EndRetCidade, string EndRetNomeResponsavel,
                       string EndRetRespTelefone, string EndEntCep, string EndEntUf, string EndEntEndereco, string EndEntNumero,
                       string EndEntComplemento, string EndEntBairro, string EndEntCidade, string EndEntNomeResponsavel,
                       string EndEntRespTelefone, string DataMaxima, string HorarioLimite, string ValorPretendido, string Observacoes,
                       string ApelidoColeta, int IdStatus, string MatTipo, string MatFragilidade, string MatDescricao, string MatPeso,
                       string MatVolume, string MatAltura, string MatLargura, DateTime DataCadastro, DateTime UltimaAtualizacao,
                       int idCliente, string HorarioLimite02, string TipoVeiculo, string DescricaoStatus)
        {
            
            this.EndRetCep             = EndRetCep;
            this.EndRetUf              = EndRetUf;
            this.EndRetEndereco        = EndRetEndereco;
            this.EndRetNumero          = EndRetNumero;
            this.EndRetComplemento     = EndRetComplemento;
            this.EndRetBairro          = EndRetBairro;
            this.EndRetCidade          = EndRetCidade;
            this.EndRetNomeResponsavel = EndRetNomeResponsavel;
            this.EndRetRespTelefone    = EndRetRespTelefone;
            this.EndEntCep             = EndEntCep;
            this.EndEntUf              = EndEntUf;
            this.EndEntEndereco        = EndEntEndereco;
            this.EndEntNumero          = EndEntNumero;
            this.EndEntComplemento     = EndEntComplemento;
            this.EndEntBairro          = EndEntBairro;
            this.EndEntCidade          = EndEntCidade;
            this.EndEntNomeResponsavel = EndEntNomeResponsavel;
            this.EndEntRespTelefone    = EndEntRespTelefone;
            this.DataMaxima            = DataMaxima;
            this.HorarioLimite         = HorarioLimite;
            this.ValorPretendido       = ValorPretendido;
            this.Observacoes           = Observacoes;
            this.ApelidoColeta         = ApelidoColeta;
            this.IdStatus              = IdStatus;
            this.MatTipo               = MatTipo;
            this.MatFragilidade        = MatFragilidade;
            this.MatDescricao          = MatDescricao;
            this.MatPeso               = MatPeso;
            this.MatVolume             = MatVolume;
            this.MatAltura             = MatAltura;
            this.MatLargura            = MatLargura;
            this.DataCadastro          = DataCadastro;
            this.UltimaAtualizacao     = UltimaAtualizacao;
            this.IdCliente             = idCliente;
            this.HorarioLimite02       = HorarioLimite02;
            this.TipoVeiculo           = TipoVeiculo;
            this.DescricaoStatus       = DescricaoStatus;
        }
        
    } 
}
