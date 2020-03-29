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
    public class MotoristaController
    {
        public HttpClient client = new HttpClient();

        public string url = "https://webapptestem.azurewebsites.net/api/motorista/";

        #region Acesso a banco

        #region INSERT
        public async Task<bool> PostMotoristaAsync(Motorista motorista)
        {
            HttpClient httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(motorista);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(url, httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao incluir dados do motorista.");
            }
            else
            {
                return result.IsSuccessStatusCode;
            }
        }
        #endregion

        #region GET - IdMotorista
        public async Task<Motorista> GetMotorista(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                string webService = url + id.ToString();

                var response = await client.GetStringAsync(webService);

                var motorista = JsonConvert.DeserializeObject<Motorista>(response);

                return motorista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region GET - ID
        public async Task<Motorista> GetMotoristaCliente(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                string webService = url + "cliente/" + id.ToString();

                var response = await client.GetStringAsync(webService);

                var motorista = JsonConvert.DeserializeObject<Motorista>(response);

                return motorista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region UPDATE 
        public async Task UpdateMotorista(Motorista motorista)
        {
            HttpClient client = new HttpClient();

            string webService = url; // + motorista.IdMotorista.ToString();

            var uri     = new Uri(string.Format(webService, motorista));
            var data    = JsonConvert.SerializeObject(motorista);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar os dados do motorista.");
            }
        }

        #endregion

        #region DELETE - ID
        public async Task DeleteMotorista(int id)
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
