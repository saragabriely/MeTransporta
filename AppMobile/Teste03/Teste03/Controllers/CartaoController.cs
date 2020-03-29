using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Teste03.Models;

namespace Teste03.Controllers
{
    public class CartaoController
    {
        public string url = "https://webapptestem.azurewebsites.net/api/cartaocredito/";

        #region Acesso a banco

        #region INSERT - CartaoCredito
        public async Task<bool> PostCartaoAsync(CartaoCredito cartao)
        {
            HttpClient httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(cartao);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(url, httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao incluir dados bancários");
            }
            else
            {
                return result.IsSuccessStatusCode;
            }
        }
        #endregion

        #region GET - CartaoCredito - ID
        public async Task<CartaoCredito> GetCartao(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                string webService = url + id.ToString();

                var response = await client.GetStringAsync(webService);

                var cartao = JsonConvert.DeserializeObject<CartaoCredito>(response);
                
                return cartao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region UPDATE - CartaoCredito
        public async Task UpdateCartao(CartaoCredito cartao)
        {
            HttpClient client = new HttpClient();

            string webService = url + cartao.IdCartao.ToString();

            var uri = new Uri(string.Format(webService, cartao.IdCartao));
            var data = JsonConvert.SerializeObject(cartao);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar os dados do cartão de crédito");
            }
        }

        #endregion

        #region DELETE - CartaoCredito - ID
        public async Task DeleteCartao(int id)
        {
            HttpClient client = new HttpClient();

            string webService = url+ id.ToString();
            var    uri = new Uri(string.Format(webService, id));

            await  client.DeleteAsync(uri);
        }
        #endregion
               
        #endregion
    }
}
