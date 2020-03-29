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
    public class MaterialController
    {
        public string url = "https://webapptestem.azurewebsites.net/api/material/";

        #region Acesso a banco

        #region INSERT
        public async Task<bool> PostMaterialAsync(Material material)
        {
            HttpClient httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(material);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(url, httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao incluir dados do material.");
            }
            else
            {
                return result.IsSuccessStatusCode;
            }
        }
        #endregion

        #region GET - IdMaterial
        public async Task<Material> GetMaterial(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                string webService = url + id.ToString();

                var response = await client.GetStringAsync(webService);

                var material = JsonConvert.DeserializeObject<Material>(response);

                return material;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region UPDATE 
        public async Task UpdateMaterial(Material material)
        {
            HttpClient client = new HttpClient();

            string webService = url; 

            var uri     = new Uri(string.Format(webService, material));
            var data    = JsonConvert.SerializeObject(material);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar os dados do material.");
            }
        }

        #endregion

        #region DELETE - ID
        public async Task DeleteMaterial(int id)
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
