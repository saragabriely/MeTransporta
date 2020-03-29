using System;
using System.Collections.Generic;
using System.Text;

namespace Teste03.Models
{
    public class AcompanhaColeta
    {
        public int      IdAcompanha {  get; set;  }
        public int      IdColeta    {  get; set;  } 
        public int      IdOrcamento {  get; set;  } 
        public int      IdCliente   {  get; set;  } 
        public int      IdMotorista {  get; set;  } 
        public DateTime DataHora    {  get; set;  }   
        public int      IdStatus    {  get; set;  } 
        public string   StatusDesc  {  get; set;  } 
                                
        public AcompanhaColeta() { }

        public AcompanhaColeta(int idOrca)
        {
            this.IdOrcamento = idOrca;
        }

        public AcompanhaColeta(int IdAcompanha,int IdColeta, int IdOrcamento, int IdCliente, int IdMotorista,
                               DateTime DataHora, int IdStatus, string statusDesc)
        {
            this.IdAcompanha   = IdAcompanha;
            this.IdColeta      = IdColeta;
            this.IdOrcamento   = IdOrcamento;
            this.IdCliente     = IdCliente;
            this.IdMotorista   = IdMotorista;
            this.DataHora      = DataHora;
            this.IdStatus      = IdStatus;
            this.StatusDesc    = StatusDesc;
        } 
        
        public AcompanhaColeta(int IdColeta, int IdOrcamento, int IdCliente, int IdMotorista,
                               DateTime DataHora, int IdStatus, string statusDesc)
        {
            this.IdColeta      = IdColeta;
            this.IdOrcamento   = IdOrcamento;
            this.IdCliente     = IdCliente;
            this.IdMotorista   = IdMotorista;
            this.DataHora      = DataHora;
            this.IdStatus      = IdStatus;
            this.StatusDesc    = StatusDesc;
        } 

    }
}
