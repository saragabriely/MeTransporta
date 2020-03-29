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
    public class AcompanhaController
    {
        public string url = "https://webapptestem.azurewebsites.net/api/acompanhacoleta/";

        #region Acesso a banco

        #region INSERT
        public async Task<bool> PostAcompanhaAsync(AcompanhaColeta acompanha)
        {
            HttpClient httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(acompanha);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(url, httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao incluir dados para acompanhar a coleta.");
            }
            else
            {
                return result.IsSuccessStatusCode;
            }
        }
        #endregion

        #region GET - LIST - Todas as coletas
        public async Task<List<AcompanhaColeta>> GetList()
        {
            HttpClient client = new HttpClient();

            try
            {
                string webService = url;

                var response = await client.GetStringAsync(webService);

                var coleta = JsonConvert.DeserializeObject<List<AcompanhaColeta>>(response);

                return coleta;
            }
            catch (Exception ex)
            { throw ex; }
        }
        #endregion

        #region GET - LIST - Todas as coletas 000000
        public async Task<List<AcompanhaColeta>> GetList(int idCliente)
        {
            try
            {
                #region Busca as coletas relacionadas ao cliente, que não estejam finalizadas

                ColetaController coletaController = new ColetaController();

                var lista_Coleta = await coletaController.GetListColetas(idCliente);

                #endregion

                // Pega todos cadastrados
                var lista = await  GetList();

                // Filtra a consulta acima de acordo com o 'idCliente'
                var coleta = lista.Where(l => l.IdCliente == idCliente).ToList();

                return coleta;
            }
            catch (Exception ex)
            { throw ex; }
        }
        #endregion

        #region GET - IdAcompanha
        public async Task<AcompanhaColeta> GetAcompanha(int idAcompanha)
        {
            HttpClient client = new HttpClient();
            try
            {
                string webService = url + idAcompanha.ToString();

                var response = await client.GetStringAsync(webService);

                var acompanha = JsonConvert.DeserializeObject<AcompanhaColeta>(response);
                
                return acompanha;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - idColeta
        public async Task<List<AcompanhaColeta>> GetAcompanha_Coleta(int idColeta)
        {
            HttpClient client = new HttpClient();

            try
            {
                /*
                string webService = url + "coleta/" + idColeta.ToString();

                var response  = await client.GetStringAsync(webService);

                var acompanha = JsonConvert.DeserializeObject<List<AcompanhaColeta>>(response); */

                var acompanha = await GetList();

                return acompanha;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - idColeta
        public async Task<List<AcompanhaColeta>> GetAcompanhaLista_Coleta(int idColeta)
        {
            HttpClient client = new HttpClient();
            try
            {
                string webService = url + "coleta/" + idColeta.ToString();

                var response = await client.GetStringAsync(webService);

                var acompanha = JsonConvert.DeserializeObject<List<AcompanhaColeta>>(response);

                return acompanha;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - idColeta
        public async Task<List<AcompanhaColeta>> GetAcompanhaLista_Coleta__(int idColeta)
        {
           // HttpClient client = new HttpClient();
            try
            {
                //  string webService = url + "coleta/" + idColeta.ToString();
                //
                //  var response = await client.GetStringAsync(webService);
                //
                //  var acompanha = JsonConvert.DeserializeObject<List<AcompanhaColeta>>(response);

                var acompanha = await GetList();

                acompanha = acompanha.Where(l => l.IdColeta == idColeta).ToList();

                return acompanha;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region GET - idColeta
        public async Task<List<AcompanhaColeta>> GetAcompanhaLista_Coleta_(int idColeta)
        {
            HttpClient client = new HttpClient();
            try
            {
                string webService = url + "coleta/" + idColeta.ToString();

                var response = await client.GetStringAsync(webService);

                var acompanha = JsonConvert.DeserializeObject<List<AcompanhaColeta>>(response);

                return acompanha;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - idCliente
        public async Task<AcompanhaColeta> GetAcompanha_Cliente(int idCliente)
        {
            HttpClient client = new HttpClient();
            try
            {
                string webService = url + "cliente/" + idCliente.ToString();

                var response = await client.GetStringAsync(webService);

                var acompanha = JsonConvert.DeserializeObject<AcompanhaColeta>(response);

                return acompanha;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - idOrcamento
        public async Task<AcompanhaColeta> GetAcompanha_Orcamento(int idOrcamento)
        {
            HttpClient client = new HttpClient();

            try
            {
                string webService = url + "orcamento/" + idOrcamento.ToString();

                var response = await client.GetStringAsync(webService);

                var acompanha = JsonConvert.DeserializeObject<AcompanhaColeta>(response);

                return acompanha;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - List - idOrcamento
        public async Task<List<AcompanhaColeta>> GetAcompanhaLista_Orcamento(int idOrcamento)
        {
            HttpClient client = new HttpClient();

            try
            {
                string webService = url + "orcamento/" + idOrcamento.ToString();

                var response = await client.GetStringAsync(webService);

                var acompanha = JsonConvert.DeserializeObject<List<AcompanhaColeta>>(response);

                return acompanha;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion        
        
        #region GET - idCliente ------------------------------------
        public async Task<List<AcompanhaColeta>> GetAcompanhaLista_Cliente(int idCliente)
        {
            HttpClient client = new HttpClient();
            try
            {
                string webService = url + "cliente/" + idCliente.ToString();

                var response = await client.GetStringAsync(webService);

                var acompanha = JsonConvert.DeserializeObject<List<AcompanhaColeta>>(response);

                return acompanha;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 
        
        #region UPDATE 
        public async Task UpdateMaterial(AcompanhaColeta acompanha)
        {
            HttpClient client = new HttpClient();

            string webService = url; 

            var uri     = new Uri(string.Format(webService, acompanha));
            var data    = JsonConvert.SerializeObject(acompanha);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar os dados.");
            }
        }

        #endregion

        #region DELETE - ID
        public async Task Delete(int id)
        {
            HttpClient client = new HttpClient();

            string webService = url + id.ToString();

            var    uri        = new Uri(string.Format(webService, id));

            await client.DeleteAsync(uri);
        }
        #endregion

        #endregion


    }
}
