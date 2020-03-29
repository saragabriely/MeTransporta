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
    public class OrcamentoController
    {
        public string url = "https://webapptestem.azurewebsites.net/api/orcamento/";
       
        #region Acesso a banco
                    
        #region INSERT - OK
        public async Task<bool> PostOrcamebtoAsync(Orcamento orcamento)
        {
            HttpClient httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(orcamento);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(url, httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao cadastrar orçamento!");
            }
            else
            {
                return result.IsSuccessStatusCode;
            }
        }
        #endregion
        
        #region GET - GetOrcamentos(int id) - Orçamento especifico
        public async Task<Orcamento> GetOrcamentos(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                string webService = url + id.ToString();

                var response = await client.GetStringAsync(webService);

                var orcamento = JsonConvert.DeserializeObject<Orcamento>(response);

                return orcamento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - GetOrcamento(int id)
        public async Task<Orcamento> GetOrcamento(int id)
        {
            HttpClient client = new HttpClient();

            try
            {
                string webService = url + id.ToString();

                var response = await client.GetStringAsync(webService);

                var orcamento = JsonConvert.DeserializeObject<Orcamento>(response);

                return orcamento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region GET - Lista - Todos os orçamentos
        public async Task<List<Orcamento>> GetListOrcamento()
        {
            HttpClient client = new HttpClient();

            try
            {
                string webService = url;

                var response = await client.GetStringAsync(webService);

                var orcamento = JsonConvert.DeserializeObject<List<Orcamento>>(response);

                return orcamento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - Lista - Cliente (IdCliente)
        public async Task<List<Orcamento>> GetListOrcamento_Cliente(int idCliente)
        {
            try
            {
                var orcamento = await GetListOrcamento();

                // Seleciona os orçamentos do cliente em questão
                var enti = orcamento.Where(i => i.IdCliente == idCliente).ToList();

                return enti;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - Lista - Cliente (IdColeta)
        public async Task<List<Orcamento>> GetListOrcamento_Cliente_(int idColeta)
        {
            try
            {
                var orcamento = await GetListOrcamento();

                // Seleciona os orçamentos do cliente em questão
                var enti = orcamento.Where(i => i.IdColeta == idColeta).ToList();
                
                return enti;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - Lista - Cliente (idColeta, idOrcaAceito, idStatus) - Atualiza IDs

        public async Task GetRecusaOrcamentos(int idColeta, int idOrcaAceito, int idStatus)
        {
            // Recebe o ID da coleta em questão, do orçamento aceito e do status que os demais orçamentos receberão
            
            List<Orcamento> _lista = new List<Orcamento>();

            List<int> _listaId = new List<int>();

            try
            {
                // Recebe a lista de orçamentos referentes a coleta em questão
                _lista = await GetListOrcamento_Cliente_(idColeta);
                
                // Seleciona apenas o ID dos orçamentos da consulta acima
                var pendentes = _lista.Select(l => l.IdOrcamento).ToList();

                // Seleciona os orçamentos descartando o que foi aceito
                var recusar = _lista.Where(l => l.IdOrcamento != idOrcaAceito).ToList();

                // Seleciona orçamento por orçamento, altera o idStatus e atualiza (update)
                for(int i = 0; i < recusar.Count; i++)
                {
                    var orcam = recusar.Where(l => l.IdStatus == 13).First();

                    orcam.IdStatus = 14;

                    await UpdateOrcamento(orcam, orcam.IdOrcamento);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - Lista - Cliente (IdColeta)
        public async Task<List<Orcamento>> GetListColetaComOrcamento(int idCliente)
        {
          //  ColetaController

            try
            {   // Todos os orçamentos
                var orcamento = await GetListOrcamento();

                // Seleciona os orçamentos do cliente em questão
                var enti = orcamento.Where(i => i.IdCliente == idCliente).ToList();

                // Pega o ID das coletas que receberam orçamentos
                var idColetas = enti.Select(l => l.IdColeta).Distinct().ToList();

                //var coletas = await 

                return enti;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        
        #region GET - GetListOrcamento_Geral - Motorista - Todos os orçamentos do motorista
        public async Task<List<Orcamento>> GetListOrcamento_Geral(int idMotorista)
        {
            try
            {
                var orcamentos = await GetListOrcamento();

                // Seleciona os orçamentos do motorista em questão
                var enti = orcamentos.Where(i => i.IdMotorista == idMotorista).ToList();

                return enti;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - GetListOrcamento(int idStatus, int idMotorista) - Orçamentos do motorista de acordo com um status
        public async Task<List<Orcamento>> GetListOrcamento(int idStatus, int idMotorista)
        { 
            try
            {
                var orcamento = await GetListOrcamento_Geral(idMotorista) ; 
                            
                var enti      = orcamento.Where(i => i.IdStatus == idStatus).ToList();
                
                return enti;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - GetListOrcamentoAceito(int idMotorista) 
        public async Task<List<Orcamento>> GetListOrcamentoAceito(int idMotorista)
        {
            ColetaController coletaController = new ColetaController();

            try
            {
                // Orcamentos aceitos
                var orcamentos = await GetListOrcamento(1, idMotorista); // status 1 - aceito

                // Captura os IDs das coletas correspondentes e filtra pelo status
                var coletaId  = orcamentos.Where(l => l.IdStatus == 1)
                                          .Select(l => l.IdColeta).ToList();

                // Captura todas as coletas
                var coletas = await coletaController.GetList();

                // Filtra as coletas
                var orca = coletas.Where(l => coletaId.Contains(l.IdColeta))
                                  .Where(l => l.IdStatus == 30)                     // status: 'aguardando motorista'
                                  .ToList();

                var orcamentoId = orca.Select(l => l.IdColeta).ToList();

                orcamentos = orcamentos.Where(l => orcamentoId.Contains(l.IdColeta)).ToList();

                return orcamentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        
        #region GET - IdMotorista + IdColeta
        public async Task<Orcamento> GetOrcamento(int idMotorista, int idColeta)
        {
            HttpClient client = new HttpClient();
            try
            {
                string link = idMotorista.ToString() + "/" + idColeta.ToString();

                // Busca todos os orçamentos
                string webService = url + link;

                var response = await client.GetStringAsync(webService);

                var orcamento = JsonConvert.DeserializeObject<Orcamento>(response);
                
                return orcamento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UPDATE
        public async Task UpdateOrcamento(Orcamento orcamento, int idOrcamento)
        {
            HttpClient client = new HttpClient();

            string webService = url;// + idVeiculo.ToString();

            orcamento.IdOrcamento = idOrcamento;

            var uri     = new Uri(string.Format(webService, orcamento));
            var data    = JsonConvert.SerializeObject(orcamento);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar os dados do orçamento.");
            }
        }

        #endregion

        #region DELETE - ID
        public async Task DeleteOrcamento(int id)
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
