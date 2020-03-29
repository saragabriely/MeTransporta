using System;
using System.Collections.Generic;
using System.Text;

namespace Teste03.Models
{ 
    public class Veiculo
    {
        public int    IdVeiculo               { get; set; }
                                              
        public int    IdMotorista             { get; set; }
        public string Placa                   { get; set; }
        public string Modelo                  { get; set; }
        public string Marca                   { get; set; }
        public string Renavam                 { get; set; }
        public string Chassi                  { get; set; }
        public int    AnoFabricacao           { get; set; }
        public int?   IdTipoVeiculo           { get; set; }
        public string CarroceriaAltura        { get; set; }
        public string CarroceriaLargura       { get; set; }
        public string CarroceriaComprimento   { get; set; }
        public string Refrigeracao            { get; set; }
        public string CapacidadeCarga         { get; set; }
        public int    IdStatus                { get; set; }
        public string TipoVeiculoDesc         { get; set; }
        public string TipoCarroceriaDesc      { get; set; }
        public int?   IdTipoCarroceria        { get; set; }
        public string DescStatus              { get; set; }

        public Veiculo() { }

        public Veiculo(int id)
        {
            this.IdVeiculo = id;
        }

        public Veiculo(int idVeiculo, int idMotorista)
        {
            this.IdVeiculo   = idVeiculo;
            this.IdMotorista = idMotorista;
        }

        public Veiculo( int IdVeiculo,  int IdMotorista,  string Placa,       string Modelo, string Marca, string Renavam,      
                        string Chassi,  int AnoFabricacao, int IdTipoVeiculo, string CarroceriaAltura, 
                        string CarroceriaLargura, string CarroceriaComprimento, string Refrigeracao,    
                        string CapacidadeCarga,   int IdStatus, string TipoVeiculoDesc,   
                        string TipoCarroceriaDesc, int IdTipoCarroceria, string DescStatus)
        {
            this.IdVeiculo             = IdVeiculo;
            this.IdMotorista           = IdMotorista;
            this.Placa                 = Placa;
            this.Modelo                = Modelo;
            this.Marca                 = Marca;
            this.Renavam               = Renavam;
            this.Chassi                = Chassi;
            this.AnoFabricacao         = AnoFabricacao;
            this.IdTipoVeiculo         = IdTipoVeiculo;
            this.CarroceriaAltura      = CarroceriaAltura;
            this.CarroceriaLargura     = CarroceriaLargura;
            this.CarroceriaComprimento = CarroceriaComprimento;
            this.Refrigeracao          = Refrigeracao;
            this.CapacidadeCarga       = CapacidadeCarga;
            this.IdStatus              = IdStatus;
            this.TipoVeiculoDesc       = TipoVeiculoDesc;
            this.TipoCarroceriaDesc    = TipoCarroceriaDesc;
            this.IdTipoCarroceria      = IdTipoCarroceria;
            this.DescStatus            = DescStatus;
        }

        public Veiculo ( int IdMotorista, string Placa, string Modelo, string Marca, string Renavam,
                        string Chassi, int AnoFabricacao, int IdTipoVeiculo, string CarroceriaAltura,
                        string CarroceriaLargura, string CarroceriaComprimento, string Refrigeracao,
                        string CapacidadeCarga, int IdStatus, string TipoVeiculoDesc,
                        string TipoCarroceriaDesc, int IdTipoCarroceria, string DescStatus)
        {
            this.IdMotorista           = IdMotorista;
            this.Placa                 = Placa;
            this.Modelo                = Modelo;
            this.Marca                 = Marca;
            this.Renavam               = Renavam;
            this.Chassi                = Chassi;
            this.AnoFabricacao         = AnoFabricacao;
            this.IdTipoVeiculo         = IdTipoVeiculo;
            this.CarroceriaAltura      = CarroceriaAltura;
            this.CarroceriaLargura     = CarroceriaLargura;
            this.CarroceriaComprimento = CarroceriaComprimento;
            this.Refrigeracao          = Refrigeracao;
            this.CapacidadeCarga       = CapacidadeCarga;
            this.IdStatus              = IdStatus;
            this.TipoVeiculoDesc       = TipoVeiculoDesc;
            this.TipoCarroceriaDesc    = TipoCarroceriaDesc;
            this.IdTipoCarroceria      = IdTipoCarroceria;
            this.DescStatus            = DescStatus;
        }

        public Veiculo( string Placa,       string Modelo, string Marca, string Renavam,      
                        string Chassi,  int AnoFabricacao, int IdTipoVeiculo, string CarroceriaAltura, 
                        string CarroceriaLargura, string CarroceriaComprimento, string Refrigeracao,    
                        string CapacidadeCarga,   int IdStatus, string TipoVeiculoDesc,   
                        string TipoCarroceriaDesc, int IdTipoCarroceria, string DescStatus)
        {
           // this.IdVeiculo          = IdVeiculo;
           // this.IdMotorista        = IdMotorista;
            this.Placa              = Placa;
            this.Modelo             = Modelo;
            this.Marca              = Marca;
            this.Renavam            = Renavam;
            this.Chassi             = Chassi;
            this.AnoFabricacao      = AnoFabricacao;
            this.IdTipoVeiculo      = IdTipoVeiculo;
            this.CarroceriaAltura   = CarroceriaAltura;
            this.CarroceriaLargura  = CarroceriaLargura;
            this.CarroceriaComprimento = CarroceriaComprimento;
            this.Refrigeracao       = Refrigeracao;
            this.CapacidadeCarga    = CapacidadeCarga;
            this.IdStatus           = IdStatus;
            this.TipoVeiculoDesc    = TipoVeiculoDesc;
            this.TipoCarroceriaDesc = TipoCarroceriaDesc;
            this.IdTipoCarroceria   = IdTipoCarroceria;
            this.DescStatus         = DescStatus;
        }
    }
}
