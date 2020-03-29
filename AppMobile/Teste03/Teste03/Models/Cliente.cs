using System;
using System.Collections.Generic;
using System.Text;

namespace Teste03.Models
{
    public class Cliente
    {
       public int    IdCliente       { get; set; }
        
        public string Cnome           { get; set; }
        public string Crg             { get; set; }
        public string Ccpf            { get; set; }
        public string Csexo           { get; set; }
        public string CdataNascto     { get; set; }
        public string Ccelular        { get; set; }
        public string Ccelular2       { get; set; }
        public string Cendereco       { get; set; }
        public string Cnumero         { get; set; }
        public string Ccomplemento    { get; set; }
        public string Cbairro         { get; set; }
        public string Ccidade         { get; set; }
        public string Ccep            { get; set; }
        public string Cuf             { get; set; }
        public string Cemail          { get; set; }
        public string Csenha          { get; set; }
        public int    IdTipoUsuario   { get; set; }
        public int    IdStatus        { get; set; }
        

        public Cliente() { }

        public Cliente (int idCliente)
        {
            this.IdCliente = idCliente;
        }

        public Cliente (string cpf)
        {
            this.Ccpf = cpf;
        }

        public Cliente(int IdCliente, string Cnome, string Crg, string Ccpf, string Csexo,
                       string CdataNascto, string Ccelular, string Ccelular2, string Cendereco, string Cnumero,
                       string Ccomplemento, string Cbairro, string Ccidade, string Ccep, string Cuf,
                       string Cemail, string Csenha, int IdTipoUsuario, int IdStatus
        )
        {
            this.IdCliente = IdCliente;
            this.Cnome = Cnome;
            this.Crg = Crg;
            this.Ccpf = Ccpf;
            this.Csexo = Csexo;
            this.CdataNascto = CdataNascto;
            this.Ccelular = Ccelular;
            this.Ccelular2 = Ccelular2;
            this.Cendereco = Cendereco;
            this.Cnumero = Cnumero;
            this.Ccomplemento = Ccomplemento;
            this.Cbairro = Cbairro;
            this.Ccidade = Ccidade;
            this.Ccep = Ccep;
            this.Cuf = Cuf;
            this.Cemail = Cemail;
            this.Csenha = Csenha;
            this.IdTipoUsuario = IdTipoUsuario;
            this.IdStatus = IdStatus;
        }

        public Cliente(string Cnome, string Crg, string Ccpf, string Csexo,
                        string CdataNascto, string Ccelular, string Ccelular2, string Cendereco, string Cnumero,
                        string Ccomplemento, string Cbairro, string Ccidade, string Ccep, string Cuf,
                        string Cemail, string Csenha, int IdTipoUsuario, int IdStatus
        )
        {
            this.Cnome = Cnome;
            this.Crg = Crg;
            this.Ccpf = Ccpf;
            this.Csexo = Csexo;
            this.CdataNascto = CdataNascto;
            this.Ccelular = Ccelular;
            this.Ccelular2 = Ccelular2;
            this.Cendereco = Cendereco;
            this.Cnumero = Cnumero;
            this.Ccomplemento = Ccomplemento;
            this.Cbairro = Cbairro;
            this.Ccidade = Ccidade;
            this.Ccep = Ccep;
            this.Cuf = Cuf;
            this.Cemail = Cemail;
            this.Csenha = Csenha;
            this.IdTipoUsuario = IdTipoUsuario;
            this.IdStatus = IdStatus;
        }

        public static implicit operator int(Cliente v)
        {
            throw new NotImplementedException();
        }
    }
}
