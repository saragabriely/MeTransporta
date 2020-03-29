using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Teste03.Models;

namespace Teste03.Controllers
{
    public class LoginController
    {
        readonly string url = "https://webapptestem.azurewebsites.net/api/login/";

        #region Acesso a banco

        #region Get - Todos

        public async Task<LoginModel> GetLogin()
        {
            HttpClient client = new HttpClient();

            try
            {
                string webService = url;

                var response = await client.GetStringAsync(webService);

                var lista = JsonConvert.DeserializeObject<LoginModel>(response);

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Get - idLogin

        public async Task<LoginModel> GetLoginId(int idLogin)
        {
            HttpClient client = new HttpClient();

            try
            {
                string webService = url + idLogin.ToString();

                var response = await client.GetStringAsync(webService);

                var lista = JsonConvert.DeserializeObject<LoginModel>(response);

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        
        #region Get List()

        public async Task<List<LoginModel>> GetList()
        {
            HttpClient client = new HttpClient();

            try
            {
                string webService = url;

                var response = await client.GetStringAsync(webService);

                var lista = JsonConvert.DeserializeObject<List<LoginModel>>(response);

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        
        #region Permite acesso 02 -- EM USO
        public async Task<LoginModel> GetLogin_(LoginModel login)
        {
            ClienteController clienteControl = new ClienteController();

            try
            {
                var cliente = await clienteControl.GetCpf(login.Ccpf);

                Session.Instance.cliente = cliente;

                #region Variáveis globais

                    Session.Instance.IdCliente     = cliente.IdCliente;
                    Session.Instance.IdTipoUsuario = cliente.IdTipoUsuario;
                    Session.Instance.Email         = cliente.Cemail;
                    Session.Instance.Senha         = cliente.Csenha;
                    Session.Instance.Cnome         = cliente.Cnome;
                    Session.Instance.Crg           = cliente.Crg;
                    Session.Instance.Ccpf          = cliente.Ccpf;
                    Session.Instance.Csexo         = cliente.Csexo;
                    Session.Instance.CdataNascto   = cliente.CdataNascto;
                    Session.Instance.Ccelular      = cliente.Ccelular;
                    Session.Instance.Ccelular2     = cliente.Ccelular2;
                    Session.Instance.Cendereco     = cliente.Cendereco;
                    Session.Instance.Cnumero       = cliente.Cnumero;
                    Session.Instance.Ccomplemento  = cliente.Ccomplemento;
                    Session.Instance.Cbairro       = cliente.Cbairro;
                    Session.Instance.Ccidade       = cliente.Ccidade;
                    Session.Instance.Ccep          = cliente.Ccep;
                    Session.Instance.Cuf           = cliente.Cuf;
                    Session.Instance.IdStatus      = cliente.IdStatus;

                    #endregion

                return login;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Permite acesso
        public async Task<LoginModel> GetLogin(string email, string senha)
        {
            HttpClient client = new HttpClient();

            ClienteController clienteControl = new ClienteController();

            try
            {
                string webService = url + "loga/?email=" + email.ToString() + "&senha=" + senha.ToString();

                var response = await client.GetStringAsync(url);

                var loga = JsonConvert.DeserializeObject<LoginModel>(response);

                if (loga == null || loga.Email == null)
                {
                    return null;
                }
                else
                {
                    var cliente = await clienteControl.GetCpf(loga.Ccpf);

                    Session.Instance.IdCliente     = cliente.IdCliente;
                    Session.Instance.IdTipoUsuario = cliente.IdTipoUsuario;
                    Session.Instance.Email         = cliente.Cemail;
                    Session.Instance.Senha         = cliente.Csenha;
                    Session.Instance.Cnome         = cliente.Cnome;
                    Session.Instance.Crg           = cliente.Crg;
                    Session.Instance.Ccpf          = cliente.Ccpf;
                    Session.Instance.Csexo         = cliente.Csexo;
                    Session.Instance.CdataNascto   = cliente.CdataNascto;
                    Session.Instance.Ccelular      = cliente.Ccelular;
                    Session.Instance.Ccelular2     = cliente.Ccelular2;
                    Session.Instance.Cendereco     = cliente.Cendereco;
                    Session.Instance.Cnumero       = cliente.Cnumero;
                    Session.Instance.Ccomplemento  = cliente.Ccomplemento;
                    Session.Instance.Cbairro       = cliente.Cbairro;
                    Session.Instance.Ccidade       = cliente.Ccidade;
                    Session.Instance.Ccep          = cliente.Ccep;
                    Session.Instance.Cuf           = cliente.Cuf;
                    Session.Instance.IdStatus      = cliente.IdStatus;

                    return loga;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - Cliente - CPF
        public async Task<LoginModel> GetCpf(string cpf)
        {
            HttpClient client = new HttpClient();
            LoginModel loginModel;

            try
            {
                string webService = url + "cpf/?cpf=" + cpf.ToString();

                var response = await client.GetStringAsync(webService);

                var loga = JsonConvert.DeserializeObject<LoginModel>(response);
                
                
                return loga;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UPDATE 
        public async Task UpdateLogin(LoginModel login)
        {
            HttpClient client = new HttpClient();

            string webService = url; 

            var uri     = new Uri(string.Format(webService, login));
            var data    = JsonConvert.SerializeObject(login);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar os dados do login.");
            }
        }

        #endregion
        
        #endregion
        
    }
}
