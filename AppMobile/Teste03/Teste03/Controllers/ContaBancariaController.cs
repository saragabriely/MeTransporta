using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Teste03.Models;

namespace Teste03.Controllers
{
    public class ContaBancariaController
    {
        public string url = "https://webapptestem.azurewebsites.net/api/contabancaria/";

        #region Acesso a banco

        #region INSERT - Conta Bancária
        public async Task<bool> PostContaAsync(ContaBancaria conta)
        {
            HttpClient httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(conta);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(url, httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao incluir dados bancários.");
            }
            else
            {
                return result.IsSuccessStatusCode;
            }
        }
        #endregion

        #region GET - Conta - ID
        public async Task<ContaBancaria> GetConta(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                string webService = url + id.ToString();

                var    response = await client.GetStringAsync(webService);

                var cartao = JsonConvert.DeserializeObject<ContaBancaria>(response);

                return cartao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - LIST
        public async Task<List<ContaBancaria>> GetListConta(int id)
        {
            HttpClient client = new HttpClient();

            List<ContaBancaria> _lista;
            List<ContaBancaria> _listaA;

            try
            {
                string webService = url;

                var response = await client.GetStringAsync(webService);

                var conta = JsonConvert.DeserializeObject<List<ContaBancaria>>(response);

                var enti = conta.Where(i => i.IdCliente == id).ToList();

                var veic = enti.Select(i => 
                        new { Texto = string.Format("{0} - {1} / {2} - {3} / {4}", 
                                i.MAgencia, i.MDigAgencia, i.MConta, i.MDigConta, i.BancoDesc),

                              Valor = i.IdContaBancaria }).ToList();

                //_lista = new List<Veiculo>(veiculo);

                _lista = new List<ContaBancaria>(enti);

                var teste = _lista.Where(i => i.IdCliente == id);

                _listaA = new List<ContaBancaria>(teste);

                return _listaA;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region UPDATE - CartaoCredito
        public async Task UpdateConta(ContaBancaria conta)
        {
            HttpClient client = new HttpClient();

            string webService = url;// + conta.IdContaBancaria.ToString();
            
            var uri     = new Uri(string.Format(webService, conta));
            var data    = JsonConvert.SerializeObject(conta);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar os dados bancários.");
            }
        }

        #endregion

        #region DELETE - CartaoCredito - ID
        public async Task DeleteConta(int id)
        {
            HttpClient client = new HttpClient();

            string webService = url + id.ToString();
            var uri = new Uri(string.Format(webService, id));

            await client.DeleteAsync(uri);
        }
        #endregion

        #endregion

    }
}
