using System;
using System.Collections.Generic;
using System.Text;

namespace Teste03.Models
{
    public class LoginModel
    {
        public int    IdLogin           { get; set; }
        public string Ccpf              { get; set; }
        public Nullable<int> IdCliente  { get; set; }
        public string Email             { get; set; }
        public string Senha             { get; set; }
        public int    IdTipoUsuario     { get; set; }
        public int    IdStatus          { get; set; }

        public LoginModel() { }

        public LoginModel(string email)
        {
            this.Email = email;
        }

        public LoginModel(string email, string senha)
        {
            this.Email = email;
            this.Senha = senha;
        }

        public LoginModel(int idLogin, string cpf, int idCliente, string email, string senha, int TipoUser, int idStatus)
        {
            this.IdLogin    = idLogin;
            this.Ccpf       = cpf;
            this.IdCliente  = IdCliente;
            this.Email      = email;
            this.Senha      = Senha;
            this.IdTipoUsuario = IdTipoUsuario;
            this.IdStatus      = idStatus;
        }

        public LoginModel(string cpf, int idCliente, string email, string senha, int TipoUser, int idStatus)
        {
            this.Ccpf       = cpf;
            this.IdCliente  = IdCliente;
            this.Email      = email;
            this.Senha      = Senha;
            this.IdTipoUsuario = IdTipoUsuario;
            this.IdStatus      = idStatus;
        }

        public LoginModel(int idLogin)
        {
            this.IdLogin = idLogin;
        }

        public LoginModel(int idLogin, int idCliente)
        {
            this.IdLogin = idLogin;
            this.IdCliente = idCliente;
        }
        
        /*
        public LoginModel(string cCpf)
        {
            this.Ccpf = cCpf;
        } */
    }
}
