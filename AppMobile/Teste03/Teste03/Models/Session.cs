using System;
using System.Collections.Generic;
using System.Text;

namespace Teste03.Models
{
    public sealed class Session
    {
        private static volatile Session instance;
        private static object sync = new Object();

        private Session() { }

        public static Session Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (sync)
                    {
                        if (instance == null)
                        {
                            instance = new Session();
                        }
                    }
                }
                return instance;
            }

        }

        /// <summary>
        /// Propriedade para o ID do usuario
        /// </summary>
        /// 
        public Cliente   cliente;
        public Motorista motorista;

        public int    IdCliente       { get; set; }
        public int    IdMotorista     { get; set; }
        public int    IdUsuario       { get; set; }
        public int    IdTipoUsuario   { get; set; }
        public string Email           { get; set; }
        public string Senha           { get; set; }
        
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
        public int    IdStatus        { get; set; }
        
        public string MnumeroCnh      { get; set; }
        public string McategoriaCnh   { get; set; }
        public string MvalidadeCnh    { get; set; }
    }
}
