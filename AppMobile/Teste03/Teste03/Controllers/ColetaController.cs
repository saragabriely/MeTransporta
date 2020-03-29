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
    public class ColetaController
    {
        public string url = "https://webapptestem.azurewebsites.net/api/coleta/";

        #region Acesso a banco

        #region GET - idColeta
        public async Task<Coleta> GetColeta(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                string webService = url + id.ToString();

                var response = await client.GetStringAsync(webService);

                var coleta = JsonConvert.DeserializeObject<Coleta>(response);

                return coleta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region GET - LIST - Todas as coletas
        public async Task<List<Coleta>> GetList()
        {
            HttpClient client = new HttpClient();

            try
            {
                string webService = url;

                var response = await client.GetStringAsync(webService);

                var coleta = JsonConvert.DeserializeObject<List<Coleta>>(response);
                
               return coleta;          
            }
            catch (Exception ex)
            { throw ex; }
        }
        #endregion

        #region Motorista

        #region GET - LIST - Id = 2 - GetListColeta_Geral(idMotorista, idStatus)
        public async Task<List<Coleta>> GetListColeta_Geral(int idMotorista, int idStatus)
        {
            // Retorna todas as coletas

            #region Variáveis e controllers
            
            List<Coleta>          _lista            = new List<Coleta>();
            List<Coleta>          _listaFiltrada;
            List<int>             lista             = new List<int>();
            List<int>             teste             = new List<int>();
            List<int>             orca              = new List<int>();
            List<ColetaVisualiza> _listaVisualizada = new List<ColetaVisualiza>();
            List<Orcamento>       _listaOrcamento   = new List<Orcamento>();

            ColetaVisualizaController visualizaController = new ColetaVisualizaController();
            OrcamentoController       orcamentoController = new OrcamentoController();

            #endregion

            try
            {
                var coleta = await GetList();
                
                #region Verifica as coletas visualizadas pelo motorista

                var listaVisualizada = await visualizaController.GetListVisualiza(idMotorista); // Captura as coletas visualizadas
                
                // Captura os IDs das coletas visualizadas pelo motorista
                teste = listaVisualizada.Select(i => i.IdColeta).ToList();

                #endregion

                #region Verifica as coletas que já foram orçadas

                var orcadas = await orcamentoController.GetListOrcamento(); // todos os orçamentos

                orcadas = orcadas.Where(l => l.IdMotorista == idMotorista).ToList(); // todas as coletas orçadas pelo motorista

                var idOrcadas = orcadas.Select(l => l.IdColeta).ToList(); // IDs das coletas orçadas

                #endregion

                if (idStatus == 0)                                      // Coletas Não Visualizadas
                {
                    #region Verifica as coletas visualizadas pelo motorista
                    
                    // Filtra as coletas não visualizadas e com status 2 (Disponíveis para envio de orçamento)
                    _listaFiltrada = coleta.Where(l => !teste.Contains(l.IdColeta))
                                           .Where(l => l.IdStatus == 2)
                                           .ToList();
                    #endregion
                    
                    return _listaFiltrada;
                }
                else if(idStatus == 1)                                  // Coletas Visualizadas
                {
                    #region Verifica as coletas visualizadas pelo motorista e que ainda não enviou orçamento
                    
                    // Filtra as coletas não visualizadas e com status 2 (Disponíveis para envio de orçamento)
                    _listaFiltrada = coleta.Where(l => teste.Contains(l.IdColeta))       // coletas visualizdas
                                           .Where(l => l.IdStatus == 2)                  // que ainda recebm orçamento
                                           .Where(l => !idOrcadas.Contains(l.IdColeta))  // que não tenha recebido orçamento ainda
                                           .ToList();
                    #endregion
                    
                    return _listaFiltrada;
                }
                else if(idStatus == 2)
                {
                    #region Busca os orçamentos

                    idStatus = 13;       // IdStatus: 13 - Aguardando aprovação
                    
                    // Captura os orçamentos enviados pelo Motorista
                    var listaOrca = await orcamentoController.GetListOrcamento(idStatus, idMotorista);
                    
                    orca = listaOrca.Select(i => i.IdColeta).ToList();

                    #endregion

                    // Seleciona os IDs das coletas que tem orçamento e posteriormente a coleta
                    _lista = coleta.Where(l => orca.Contains(l.IdColeta)).ToList();

                    return _lista;
                }

                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - LIST - GetListColeta_Status(idMotorista)
        public async Task<List<Coleta>> GetListColeta_Status(int id)
        {
            // Retorna todas as coletas de acordo com o id selecionado
            
            List<Coleta> _lista;
            List<Coleta> _listaFiltrada;
            
            try
            {
                _lista = await GetListColeta_Geral(id, 0);
                
                _listaFiltrada = _lista.Where(i => i.IdStatus == 2).ToList();

                return _listaFiltrada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - LIST - GetListColetaMotorista(idMotorista, idStatus)
        public async Task<List<Coleta>> GetListColetaMotorista(int idMotorista, int idStatus) // Retorna coletas com o id selecionado
        {
            #region Controlers e variáveis

            ColetaVisualizaController visualizaControl = new ColetaVisualizaController();

            HttpClient client = new HttpClient();

            List<Coleta> _listaFiltrada;

            List<ColetaVisualiza> _listaVisualiza;
            
            #endregion

            try
            {   // Seleciona todas as coletas
                var lista = await GetListColeta_Geral(idMotorista, idStatus);
                
                // Filtro 01 - Filtra as coletas pelo status 02 (disponível para envio de orçamentos)
                _listaFiltrada = lista.Where(i => i.IdStatus == 2).ToList();
                
                if(idStatus == 0)                               // Coletas disponíveis ainda não visualizadas
                {
                    #region Filtra coletas ainda não visualizadas

                    // Filtro 02 - Filtra as coletas relacionadas ao motorista
                    _listaVisualiza = await visualizaControl.GetListVisualiza(idMotorista);

                    // Filtro 03 - Filtra as coletas não visualizadas
                    #region Seleciona as coletas que não forem visualizadas
                    
                    // Seleciona os IDs das coletas visualizadas
                    var listaVisualizaFiltrada = _listaVisualiza
                                                 .Select(i => new { Valor = i.IdColeta }).ToList();

                    // Transforma a lista anonima de status acima, em uma lista de inteiros (para a comparação)
                    // List<int> listStatus = new List<int>(listaVisualizaFiltrada);

                    // Filtro 04 - Filtra as coletas que foram visualizadas
                    // var _listaFiltra = _listaFiltrada.Where(i => !listStatus.Contains(i.IdColeta));

                    //var teste = _listaFiltra.Where(i => listStatus.Contains(i.IdColeta)).ToList();
                    

                    return _listaFiltrada;

                    #endregion

                    #endregion
                }

                return _listaFiltrada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - LIST - MOTORISTA (idStatus)
        public async Task<List<Coleta>> GetListColetaMotorista_2(int idStatus)
        {
            try
            {
                var coleta = await GetList();
                
               var enti = coleta.Where(i => i.IdStatus == 2).ToList();     // Status 2 = Aguardado orçamento

               return enti;          
            }
            catch (Exception ex) {  throw ex;  }
        }
        #endregion

        #region GET - LIST - Coletas - Filtro - Histórico
        public async Task<List<Coleta>> GetList_Historico(int idMotorista)
        {
            OrcamentoController orcaControl = new OrcamentoController();
            
            try
            {
                // Captura todos orçamentos 
                var orcamentos = await orcaControl.GetListOrcamento();

                // Captura orçamentos realizados pelo motorista
                orcamentos = orcamentos.Where(i => i.IdMotorista == idMotorista).ToList();

                // Captura ID das coletas da consulta acima
                var idColetas = orcamentos.Select(i => i.IdColeta).ToList();

                // Todas as coletas
                var coletas = await GetList();

                // Filtra as coletas de acordo com o List<int> idColetas
                coletas = coletas.Where(l => idColetas.Contains(l.IdColeta)).ToList();

                /*
                if(idEscolha == 0)           // todas as coletas
                {
                    return coletas;
                }

                else if(idEscolha == 1)     // coletas em andamento
                {
                    coletas = coletas.Where(l => l.IdStatus == 8).ToList();

                    return coletas;
                }

                else if(idEscolha == 2)     // coletas realizadas
                {
                    coletas = coletas.Where(l => l.IdStatus == 10).ToList();

                    return coletas;
                }
                else if(idEscolha == 3)     // coletas canceladas
                {
                    coletas = coletas.Where(l => l.IdStatus == 6).ToList();

                    return coletas;
                } */

                return coletas;
            }
            catch (Exception ex)
            { throw ex; }
        }
        #endregion

        #endregion

        #region Cliente 

        #region INSERT
        public async Task<bool> PostColetaAsync(Coleta coleta)
        {
            HttpClient httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(coleta);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(url, httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao incluir dados da coleta.");
            }
            else
            {
                return result.IsSuccessStatusCode;
            }
        }
        #endregion

        #region GET - GetListColetas(int idCliente)  - Todos as coletas
        public async Task<List<Coleta>> GetListColetas(int idCliente)
        {
            // Seleciona todas as coletas

            #region Variáveis e controllers

            List<Coleta> _lista = new List<Coleta>();

            #endregion

            try
            {
                var coleta = await GetList();

                _lista = coleta.Where(l => l.IdCliente == idCliente).ToList();

                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - GetListColeta_Orcamento(int idCliente)  - Coletas que possuem orçamento
        public async Task<List<Coleta>> GetListColetaComOrcamento(int idCliente)
        {
            // Seleciona as coletas que possuem orçamentos e que estão com o Status = 01

            #region Variáveis e controllers

            List<Coleta>    _listaFiltrada;
            List<Orcamento> _listaOrca;

            Orcamento           orcamento           = new Orcamento();

            OrcamentoController orcamentoController = new OrcamentoController();

            #endregion

            try
            {
                var coleta = await GetList();

                // Retorna todos os orçamentos relacionados ao cliente
                _listaOrca = await orcamentoController.GetListOrcamento_Cliente(idCliente);

                // Retorna os IDs das coletas
                var retornaId = _listaOrca.Select(i => i.IdColeta).Distinct().ToList();
                
                _listaFiltrada = coleta.Where(l => retornaId.Contains(l.IdColeta)).ToList();

                return _listaFiltrada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - GetListColeta_Orcamento(int idCliente)  - Coletas que possuem orçamento
        public async Task<List<Coleta>> GetListColeta_Orcamento(int idCliente)
        {
            // Seleciona as coletas que possuem orçamentos e que estão com o Status = 01

            #region Variáveis e controllers

            List<Coleta>    _listaFiltrada;
            List<Orcamento> _listaOrca;

            Orcamento           orcamento           = new Orcamento();

            OrcamentoController orcamentoController = new OrcamentoController();

            #endregion

            try
            {
                var coleta = await GetList();

                // Retorna todos os orçamentos relacionados ao cliente
                _listaOrca = await orcamentoController.GetListOrcamento_Cliente(idCliente);

                // Retorna os IDs das coletas
                var retornaId = _listaOrca.Select(i => i.IdColeta).Distinct().ToList();
                
                _listaFiltrada = coleta.Where(l => retornaId.Contains(l.IdColeta))
                                       .Where(l => l.IdStatus == 2)                // Status: 2 - Aguardando Orçamento
                                       .ToList();

                return _listaFiltrada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region GET - GetListColeta(int id)
        public async Task<List<Coleta>> GetListColeta(int id)
        {
            try
            {
                var coleta = await GetList();

                var enti   = coleta.Where(i => i.IdCliente == id).ToList();

                return enti;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - LIST - CLIENTE - ESPECIFICO
        public async Task<List<Coleta>> GetListColetaEsp(int idCliente, int idStatus)
        {
            try
            {
                // Seleciona todas as coletas relacionadas a este cliente
                var enti = await GetListColeta(idCliente);
                
                var teste = enti.Where(i => i.IdCliente == idCliente).Where(i => i.IdStatus == idStatus).ToList();

                return teste;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - LIST - Coletas em andamento
        public async Task<List<Coleta>> GetList_(int idCliente)
        {
            AcompanhaController controller = new AcompanhaController();

            try
            {   // Lista de coletas (todas)
                var coleta = await GetList();
                              
                // Filtra pelo Id do cliente e do Status
                var filtra = coleta.Where(l => l.IdCliente == idCliente)
                                   .Where(l => l.IdStatus == 10)  // Finalizada(o)
                                   .ToList();
                    
                // Captura os IDs da consulta acima 
                var filtraId = coleta.Select(l => l.IdColeta).ToList();

                // Pega a lista das coletas cadastradas em 'AcompanhaColeta' (que já foram iniciadas)
                var acompanha = await controller.GetList();

                // Seleciona o Id das coletas (de acordo com o ID do cliente)
                var acompanhaID = acompanha.Where(l => l.IdCliente == idCliente)
                                           .Select(l => l.IdColeta)
                                           .Distinct()
                                           .ToList();

                // Filtra as coletas que foram iniciadas
                var filtraColeta = filtra.Where(l => acompanhaID.Contains(l.IdColeta)).ToList();
                
                return filtraColeta;
            }
            catch (Exception ex)
            {  throw ex;  }
        }
        #endregion

        #region GET - LIST - Coletas em andamento
        public async Task<List<Coleta>> GetList(int idCliente)
        {
            AcompanhaController controller = new AcompanhaController();

            try
            {   // Lista de coletas (todas)
                var coleta = await GetList();
                              
                // Filtra pelo Id do cliente e do Status
                var filtra = coleta.Where(l => l.IdCliente == idCliente)
                                   .Where(l => l.IdStatus != 10)  // Finalizada(o)
                                   .Where(l => l.IdStatus != 60)  // Coleta finalizada
                                   .Where(l => l.IdStatus != 9)   // Excluida(o)
                                   .ToList();
                    
                // Captura os IDs da consulta acima 
                var filtraId = coleta.Select(l => l.IdColeta).ToList();

                // Pega a lista das coletas cadastradas em 'AcompanhaColeta' (que já foram iniciadas)
                var acompanha = await controller.GetList();

                // Seleciona o Id das coletas (de acordo com o ID do cliente)
                var acompanhaID = acompanha.Where(l => l.IdCliente == idCliente)
                                           .Select(l => l.IdColeta)
                                           .Distinct()
                                           .ToList();

                // Filtra as coletas que foram iniciadas
                var filtraColeta = filtra.Where(l => acompanhaID.Contains(l.IdColeta)).ToList();
                
                return filtraColeta;
            }
            catch (Exception ex)
            {  throw ex;  }
        }
        #endregion

        #region OLDD
        /*
         * #region GET - LIST - Coletas em andamento
        public async Task<List<Coleta>> GetList(int idCliente)
        {
            AcompanhaController controller = new AcompanhaController();

            try
            {   // Lista de coletas (todas)
                var coleta = await GetList();

                // Filtra pelo Id do cliente e do Status
                var filtra = coleta.Where(l => l.IdCliente == idCliente)
                                   .Where(l => l.IdStatus != 10)  // Finalizada(o)
                                   .Where(l => l.IdStatus != 60)  // Coleta finalizada
                                   .Where(l => l.IdStatus != 9)   // Excluida(o)
                                   .ToList();

                // Captura os IDs da consulta acima 
                var filtraId = coleta.Select(l => l.IdColeta).ToList();

                // Pega a lista das coletas cadastradas em 'AcompanhaColeta' (que já foram iniciadas)
                var acompanha = await controller.GetList();

                // Seleciona o Id das coletas (de acordo com o ID do cliente)
                var acompanhaID = acompanha.Where(l => l.IdCliente == idCliente)
                                           .Select(l => l.IdColeta)
                                           .Distinct()
                                           .ToList();

                // Filtra as coletas que foram iniciadas
                var filtraColeta = filtra.Where(l => acompanhaID.Contains(l.IdColeta)).ToList();
                
                return filtraColeta;
            }
            catch (Exception ex)
            {  throw ex;  }
        }
        #endregion
         
             */
        #endregion

        #region Old

        /*
         #region GET - GetListColetas_Acompanha(int idCliente)  - Coletas que estão em andamento
        public async Task<List<Coleta>> GetListColetas_Acompanha(int idCliente)
        {
            AcompanhaController   acompanhaControll = new AcompanhaController();

            try
            {   // Captura todas as coletas do cliente
                var coleta = await GetListColetas(idCliente);

                // Captura todas as coletas em andamento do cliente
                var lista = await acompanhaControll.GetAcompanhaLista_Cliente(idCliente);

                // Captura os IDs da consulta acima 
                var listaId = lista.Select(l => l.IdColeta).ToList();

                // Filtra a lista pelos IDs das coletas que estão em andamento
                var coletas = coleta.Where(l => listaId.Contains(l.IdColeta)).ToList();

                return coletas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
             
             */


        #endregion


        #region UPDATE 
        public async Task UpdateColeta(Coleta coleta)
        {
            HttpClient client = new HttpClient();

            string webService = url;

            var uri     = new Uri(string.Format(webService, coleta));
            var data    = JsonConvert.SerializeObject(coleta);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar os dados da coleta.");
            }
        }

        #endregion

        #region DELETE - ID
        public async Task DeleteColeta(int id)
        {
            HttpClient client = new HttpClient();

            string webService = url + id.ToString();

            var uri = new Uri(string.Format(webService, id));

            await client.DeleteAsync(uri);
        }
        #endregion

        #region OLD

        /* #region INSERT
        public async Task<bool> PostColetaAsync(Coleta coleta)
        {
            HttpClient httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(coleta);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(url, httpContent);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao incluir dados da coleta.");
            }
            else
            {
                return result.IsSuccessStatusCode;
            }
        }
        #endregion

        #region GET - GetListColeta_Orcamento(int idCliente)  - Coletas que possuem orçamento
        public async Task<List<Coleta>> GetListColeta_Orcamento(int idCliente)
        {
            // Seleciona as coletas que possuem orçamentos e que estão com o Status = 01

            HttpClient client = new HttpClient(); 
            
            #region Variáveis e controllers

            List<Coleta>    _listaFiltrada;
            List<Orcamento> _listaOrca;

            Orcamento           orcamento           = new Orcamento();

            OrcamentoController orcamentoController = new OrcamentoController();

            #endregion

            try
            {
                string webService = url;

                var response = await client.GetStringAsync(webService);

                var coleta = JsonConvert.DeserializeObject<List<Coleta>>(response);

                // Retorna todos os orçamentos relacionados ao cliente
                _listaOrca = await orcamentoController.GetListOrcamento_Cliente(idCliente);

                // Retorna os IDs das coletas
                var retornaId = _listaOrca.Select(i => i.IdColeta).Distinct().ToList();
                
                _listaFiltrada = coleta.Where(l => retornaId.Contains(l.IdColeta))
                                       .Where(l => l.IdStatus == 2)                // Status: 2 - Aguardando Orçamento
                                       .ToList();

                return _listaFiltrada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region GET - GetListColeta(int id)
        public async Task<List<Coleta>> GetListColeta(int id)
        {
            HttpClient client = new HttpClient();

            List<Coleta> _lista;

            try
            {
                string webService = url;

                var response = await client.GetStringAsync(webService);

                var coleta   = JsonConvert.DeserializeObject<List<Coleta>>(response);

                var enti     = coleta.Where(i => i.IdCliente == id).ToList();

                var veic     = 
                    enti.Select(i => new { Texto = string.Format("{0} - {1} - {2}", i.IdColeta, i.ApelidoColeta, i.IdStatus),
                    Valor = i.IdColeta }).ToList();
                
                _lista = new List<Coleta>(enti);

                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GET - LIST - CLIENTE - ESPECIFICO
        public async Task<List<Coleta>> GetListColetaEsp(int idCliente, int idStatus)
        {
            HttpClient client = new HttpClient();

            List<Coleta> _lista, _listaA;

            try
            {
                string webService = url;

                var response = await client.GetStringAsync(webService);

                var coleta = JsonConvert.DeserializeObject<List<Coleta>>(response);

                // Seleciona todas as coletas relacionadas a este cliente
                var enti = coleta.Where(i => i.IdCliente == idCliente).ToList();

                var veic =  enti
                           // .Where(i => i.IdStatus == idStatus)
                            .Select(i => new {
                                Texto = string.Format("{0} - {1} - {2}", i.IdColeta, i.ApelidoColeta, i.IdStatus),
                                Valor = i.IdColeta
                            }).ToList();

                _lista = new List<Coleta>(enti);

                var teste = _lista.Where(i => i.IdCliente == idCliente);

                var lista = teste.Where(i => i.IdStatus == idStatus);

                _listaA = new List<Coleta>(lista);

                return _listaA;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region UPDATE 
        public async Task UpdateColeta(Coleta coleta)
        {
            HttpClient client = new HttpClient();

            string webService = url;

            var uri     = new Uri(string.Format(webService, coleta));
            var data    = JsonConvert.SerializeObject(coleta);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar os dados da coleta.");
            }
        }

        #endregion

        #region DELETE - ID
        public async Task DeleteColeta(int id)
        {
            HttpClient client = new HttpClient();

            string webService = url + id.ToString();

            var uri = new Uri(string.Format(webService, id));

            await client.DeleteAsync(uri);
        }
        #endregion
         
         
         */

        #endregion

        #endregion

        #endregion

    }
}
