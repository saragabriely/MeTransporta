using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Teste03.Models;
using Teste03.Views;
using Teste03.Services;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using Login = Teste03.Views.Login;

namespace Teste03.Services
{
    public class DataService
    {
        readonly string url = "https://webapptestem.azurewebsites.net/api/";

        #region INSERT

        #region Teste - INSERT
        /* public async Task<bool> PostAsync(Teste teste)
         {
             HttpClient httpClient = new HttpClient();

             var json = JsonConvert.SerializeObject(teste);

             HttpContent httpContent = new StringContent(json);

             httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

             var result = await httpClient.PostAsync(url + "teste/", httpContent);

             if (!result.IsSuccessStatusCode)
             {
                 throw new Exception("Erro ao incluir teste");
             }
             else
             {
                 return result.IsSuccessStatusCode;
             }
         }*/
        #endregion

        #region Insert - Cliente
        public async Task<bool> PostAsync(Cliente cliente)
        {
            HttpClient httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(cliente);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            var result = await httpClient.PostAsync(url + "cliente/", httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao incluir cliente!");
            }
            else
            {
                return result.IsSuccessStatusCode;
            }
        }
        #endregion

        #region Insert - Cartao
        public async Task<bool> PostCartaoAsync(CartaoCredito cartao)
        {
            HttpClient httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(cartao);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(url + "cartaocredito/", httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao incluir cartao!");
            }
            else
            {
                return result.IsSuccessStatusCode;
            }
        }
        #endregion

        #endregion

        #region GetList

        #endregion

        #region LOGIN

        #region Permite acesso
        public async Task<LoginModel> GetLogin(string email, string senha)
        {
            HttpClient client = new HttpClient();
            LoginModel login;

            try
            {
                string webService = url + "login/loga/?email=" + email.ToString() + "&senha=" + senha.ToString();
                
                //if(await client.GetStringAsync(webService) == )
                if((await client.GetStringAsync(webService) == null))
                {
                   return null;
                }
                else
                {
                    var response = await client.GetStringAsync(webService);

                    var loga = JsonConvert.DeserializeObject<LoginModel>(response);

                    if (loga == null)
                    {
                        return null;
                    }
                    return loga;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #endregion

        /*
        public GenericDataService<Cliente>{

            HttpClient client; // = new HttpClient();
            //string url = "https: //webapimt2.azurewebsites.net/api/cliente/";
            string url = "https://webapimt2.azurewebsites.net/api/" + typeof(Cliente).Name.ToString() + "/";

            public async Task<List<Cliente>> Get()
            {
                try
                {
                   // string url = " https:/ /webapimt2.azurewebsites .net/api/cliente/ ";
                    var httpClient = new HttpClient();
                    var response   = await client.GetStringAsync(url);
                    var clientes   = JsonConvert.DeserializeObject<List<Cliente>>(response);
                    return clientes;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        /*
        public async Task AddClienteAsync(Cliente cliente)
        {
            try
            {
                string url = "https:/ /webapimt2.azurewebsites.net/api/cliente/";

                var uri = new Uri(string.Format(url, cliente.idCliente));

                var data = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao cadastrar cliente!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } * /

            public async Task<bool> PutAsync(int id, Cliente cliente)
            {
                var httpClient = new HttpClient();

                var json = JsonConvert.SerializeObject(t);

                HttpContent httpContent = new StringContent(json);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = await httpClient.PutAsync(url + id, httpContent);

                return result.IsSuccessStatusCode;
            }

        /*
        public async Task UpdateClienteAsync(Cliente cliente)
            {
                //string url = "https:/ /webapimt2.azurewebsites.net/api/cliente/{0}";

                var uri = new Uri(string.Format(url, cliente.idCliente));

                var data = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PutAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao atualizar cadastro de cliente!");
                }
            }
         * /
            public async Task DeletaClienteAsync(Cliente cliente)
            {
                string url = "https:/ /webapimt2.azurewebsites.net/api/cliente/{0}";

                var uri = new Uri(string.Format(url, cliente.idCliente));

                await client.DeleteAsync(uri);
            }
        }
        */
    }
}
