using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebAppTeste.Models;

namespace WebAppTeste.Controllers
{
    public class OrcamentoController : ApiController
    {
        private MTContext db = new MTContext();

        #region GET ALL - OK
        [Route("api/orcamento")]
        public IEnumerable<Orcamento> GetAll()
        {
            return db.Orcamentos.ToList();
        }
        #endregion

        #region GET - ID - OK
        [Route("api/orcamento/{id}")]
        [HttpGet]
        public HttpResponseMessage GetOrcamento(int id)
        {
            db.Database.Connection.Open();

            var entity = db.Orcamentos.FirstOrDefault(e => e.IdOrcamento == id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Orcamento com o Id = "
                        + id.ToString() + " não foi encontrado.");
            }
        }
        #endregion

        #region GET - IdMotorista + IdColeta
        [Route("api/orcamento/{idMotorista}/{idColeta}")]
        [HttpGet]
        public HttpResponseMessage GetOrcamento(int idMotorista, int idColeta)
        {
        
            db.Database.Connection.Open();

            //var entity = db.Orcamentos.FirstOrDefault(e => e.IdOrcamento == id);

            var entity = db.Orcamentos.Where(i => i.IdColeta    == idColeta)
                                      .Where(i => i.IdMotorista == idMotorista)
                                      .ToList();

            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Combinação não encontrada.");
            }
        }
        #endregion

        #region INSERT - OK

        [Route("api/orcamento")]
        [HttpPost]
        [ResponseType(typeof(Orcamento))]
        public IHttpActionResult Post([FromBody]Orcamento orcamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orcamentos.Add(orcamento);
            db.SaveChanges();

            return CreatedAtRoute("Default", new { id = orcamento.IdColeta }, orcamento);
        }
        #endregion
        
        #region DELETE - OK
        
        [Route("api/orcamento/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entity = db.Orcamentos.FirstOrDefault(e => e.IdOrcamento == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Orcamento com o ID = "
                        + id.ToString() + " não encontrado para deletar.");
                }
                else
                {
                    db.Orcamentos.Remove(entity);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        #endregion
        
        #region UPDATE 

        [Route("api/orcamento")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]Orcamento orcamento)
        {
            var entity = db.Orcamentos.FirstOrDefault(e => e.IdOrcamento == orcamento.IdOrcamento);

            if (entity != null)
            {
                #region Atributos
                
                entity.IdCliente      = orcamento.IdCliente;
                entity.IdMotorista    = orcamento.IdMotorista;
                entity.Valor          = orcamento.Valor;
                entity.DataCadastro   = orcamento.DataCadastro;
                entity.IdStatus       = orcamento.IdStatus;
                entity.DataAceite     = orcamento.DataAceite;
                entity.DataRecusa     = orcamento.DataRecusa;
                entity.Observacoes    = orcamento.Observacoes;
                entity.Visualiza      = orcamento.Visualiza;
                entity.IdVeiculoUsado = orcamento.IdVeiculoUsado;
                entity.DescStatus     = orcamento.DescStatus;

                #endregion

                db.SaveChanges();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, entity));
            }
            else
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound,
                    "Orçamento não localizado para alteração."));
        }
        #endregion
        
    }
}