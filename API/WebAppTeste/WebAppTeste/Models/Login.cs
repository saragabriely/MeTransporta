using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppTeste.Models
{
    [Table("Login")]
    public class Login
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int           IdLogin       { get; set; }
        public string        Ccpf          { get; set; }
        public Nullable<int> IdCliente     { get; set; }
        public string        Email         { get; set; }
        public string        Senha         { get; set; }
        public int           IdTipoUsuario { get; set; }
        public int           IdStatus      { get; set; }

        public Login() { }

        public Login(string email)
        {
            this.Email = email;
        }

        public Login(string email, string senha)
        {
            this.Email = email;
            this.Senha = senha;
        }

        public Login(int idLogin, string cpf, int idCliente, string email, int TipoUser, int idStatus)
        {
            this.IdLogin        = idLogin;
            this.Ccpf           = cpf;
            this.IdCliente      = IdCliente;
            this.Email          = email;
            this.Senha          = Senha;
            this.IdTipoUsuario  = IdTipoUsuario;
            this.IdStatus       = idStatus;
        }
        
        public Login (int idLogin)
        {
            this.IdLogin = idLogin;
        }

        public Login (int idLogin, int idCliente)
        {
            this.IdLogin = idLogin;
            this.IdCliente = idCliente;
        }

        /*
        public Login(string cCpf)
        {
            this.Ccpf = cCpf;
        }*/
    }
}