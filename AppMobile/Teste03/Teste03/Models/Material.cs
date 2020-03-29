using System;
using System.Collections.Generic;
using System.Text;

namespace Teste03.Models
{
    public class Material
    {
        public int       IdMaterial        { get; set; }

        public int?      IdColeta          { get; set; }
        public int       IdCliente         { get; set; }
        public string    MatTipo           { get; set; }
        public string    MatFragilidade    { get; set; }
        public string    MatDescricao      { get; set; }
        public string    MatPeso           { get; set; }
        public string    MatVolume         { get; set; }
        public string    MatAltura         { get; set; }
        public string    MatLargura        { get; set; }
        public DateTime  DataCadastro      { get; set; }
        public DateTime? UltimaAtualizacao { get; set; }
        public int       IdStatus          { get; set; }

        public Material() { }

        public Material (int idMaterial)
        {
            this.IdMaterial = idMaterial;
        }


        public Material(int IdMaterial, int IdColeta, int IdCliente, string MatTipo, string MatFragilidade, 
                        string MatDescricao, string MatPeso, string MatVolume, string MatAltura, string MatLargura,
                        DateTime DataCadastro, DateTime UltimaAtualizacao, int idStatus )
        {
            this.IdMaterial        = IdMaterial;
            this.IdColeta          = IdColeta;
            this.IdCliente         = IdCliente;
            this.MatTipo           = MatTipo;
            this.MatFragilidade    = MatFragilidade;
            this.MatDescricao      = MatDescricao;
            this.MatPeso           = MatPeso;
            this.MatVolume         = MatVolume;
            this.MatAltura         = MatAltura;
            this.MatLargura        = MatLargura;
            this.DataCadastro      = DataCadastro;
            this.UltimaAtualizacao = UltimaAtualizacao;
            this.IdStatus          = idStatus;
        }

        public Material(int IdColeta, int IdCliente, string MatTipo, string MatFragilidade, 
                        string MatDescricao, string MatPeso, string MatVolume, string MatAltura, string MatLargura,
                        DateTime DataCadastro, DateTime UltimaAtualizacao, int idStatus )
        {
            this.IdColeta          = IdColeta;
            this.IdCliente         = IdCliente;
            this.MatTipo           = MatTipo;
            this.MatFragilidade    = MatFragilidade;
            this.MatDescricao      = MatDescricao;
            this.MatPeso           = MatPeso;
            this.MatVolume         = MatVolume;
            this.MatAltura         = MatAltura;
            this.MatLargura        = MatLargura;
            this.DataCadastro      = DataCadastro;
            this.UltimaAtualizacao = UltimaAtualizacao;
            this.IdStatus          = idStatus;   
        }

        public Material(int IdMaterial, DateTime UltimaAtualizacao )
        {
            this.IdMaterial        = IdMaterial;
            this.UltimaAtualizacao = UltimaAtualizacao;
        }

        public Material(int IdMaterial, int idStatus)
        {
            this.IdMaterial = IdMaterial;
            this.IdStatus   = idStatus;
        }

    }
}
