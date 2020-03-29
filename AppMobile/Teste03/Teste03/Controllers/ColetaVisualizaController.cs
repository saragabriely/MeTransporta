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
    public class ColetaVisualizaController
    {
        public string url = "https://webapptestem.azurewebsites.net/api/coletavisualiza/";

        #region Acesso a banco
                    
        #region INSERT - OK
        public async Task<bool> PostVisualizaAsync(ColetaVisualiza visualiza)
        {
            HttpClient httpClient = new HttpClient();
            
            var json = JsonConvert.SerializeObject(visualiza);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(url, httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao cadastrar visualização!");
            }
            else
            {
                return result.IsSuccessStatusCode;
            }
        }
        #endregion

        #region GET - LIST - ID Motorista
        public async Task<List<ColetaVisualiza>> GetListVisualiza(int idMotorista)
        {
            HttpClient client = new HttpClient();

            List<ColetaVisualiza> _lista;

            try
            {
                string webService = url;

                var response = await client.GetStringAsync(webService);

                var visualiza = JsonConvert.DeserializeObject<List<ColetaVisualiza>>(response);

                var enti = visualiza.Where(i => i.IdMotorista == idMotorista).ToList();

                _lista = new List<ColetaVisualiza>(enti);

                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - LIST - IdMotorista + IdColeta
        public async Task<List<ColetaVisualiza>> GetListVisualiza_(int idMotorista, int idColeta)
        {
            HttpClient client = new HttpClient();
            
            List<ColetaVisualiza> _lista;

            try
            {   
                string webService = url;

                var response = await client.GetStringAsync(webService);
                
                var visualiza = JsonConvert.DeserializeObject<List<ColetaVisualiza>>(response);
                
                var enti      = visualiza.Where(i => i.IdMotorista == idMotorista)
                                        .Where(i => i.IdColeta    == idColeta)
                                        .ToList();
                
                _lista      = new List<ColetaVisualiza>(enti);
                                
                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region GET - ID
        public async Task<ColetaVisualiza> GetVisualiza(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                string webService = url + id.ToString();

                var response      = await client.GetStringAsync(webService);

                var visualiza     = JsonConvert.DeserializeObject<ColetaVisualiza>(response);
                
                return visualiza;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
       
        #endregion
    }
}
