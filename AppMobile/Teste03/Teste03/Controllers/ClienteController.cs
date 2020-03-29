using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Teste03.Models;

namespace Teste03.Controllers
{
    public class ClienteController
    {
        readonly string url = "https://webapptestem.azurewebsites.net/api/cliente/";

        #region Acesso a banco
        
        #region INSERT - Cliente
        public async Task<bool> PostAsync(Cliente cliente)
        {
            string cpf = cliente.Ccpf;

            HttpClient httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(cliente);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(url, httpContent);

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

        #region GET - Cliente - Todos
        public async Task<List<Cliente>> GetList()
        {
            HttpClient client = new HttpClient();

            try
            {
                string webService = url;

                var    response   = await client.GetStringAsync(webService);
                                  
                var    cliente    = JsonConvert.DeserializeObject<List<Cliente>>(response);
                               
                return cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region GET - Cliente - ID
        public async Task<Cliente> GetCliente(int id)
        {
            HttpClient client = new HttpClient();

            try
            {
                string webService = url + id.ToString();

                var    response   = await client.GetStringAsync(webService);
                                  
                var    cliente    = JsonConvert.DeserializeObject<Cliente>(response);
                               
                return cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - Cliente - CPF
        public async Task<Cliente> GetCpf(string cpf)
        {
            HttpClient client = new HttpClient();

            try
            {
                string webService = url + "cpf/?cpf=" + cpf.ToString();

                var    response   = await client.GetStringAsync(webService);
                                  
                var    loga       = JsonConvert.DeserializeObject<Cliente>(response);
                                
                return loga;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - Cliente - CPF - TENTATIVA
        /*public async Task<Cliente> GetCpf(string cpf)
        {
            HttpClient client = new HttpClient();
            Cliente cliente;
            /*
            try
            { * /
                string webService = url + "cpf/?cpf=" + cpf.ToString();

               // var response = await client.GetStringAsync(webService);

                if ((await client.GetStringAsync(webService)) == HttpStatusCode.NotFound.ToString())
                {
                    return null;
                }
                else
                {
                    var response = await client.GetStringAsync(webService);

                    var loga = JsonConvert.DeserializeObject<Cliente>(response);

                    if (loga == null)
                    {
                        return null;
                    }
                    else
                    {
                        cliente = new Cliente(
                             loga.Cnome, loga.Crg, loga.Ccpf, loga.Csexo, loga.CdataNascto, loga.Ccelular,
                             loga.Ccelular2, loga.Cendereco, loga.Cnumero, loga.Ccomplemento, loga.Cbairro,
                             loga.Ccidade, loga.Ccep, loga.Cuf, loga.Cemail, loga.Csenha, loga.IdTipoUsuario, loga.IdStatus
                        );
                    }
                    return loga;
                }
                
            /*}
            catch (Exception ex)
            {
                throw ex;
            } * /
        } */
        #endregion

        #region Update - Cliente - Desativa status

        public async Task UpdateCliente_(int idCliente, int idStatus)
        {
          // var cliente = 
            
        }
        #endregion

        #region Update - Cliente

        public async Task UpdateCliente(Cliente cliente, int idCliente)
        {
            HttpClient client = new HttpClient();

            cliente.IdCliente = idCliente;

            string webService = url;// + idCliente.ToString();

            var uri     = new Uri(string.Format(url, cliente.IdCliente));
            var data    = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar o cadastro do cliente");
            }
        }
        #endregion

        #region Deletar - Cliente - ID
        public async Task DeleteCliente(int id)
        {
            HttpClient client = new HttpClient();

            string webService = url + id.ToString();
            var    uri        = new Uri(string.Format(webService, id));

            await  client.DeleteAsync(uri);
        }
        #endregion

        #endregion
    }
}
