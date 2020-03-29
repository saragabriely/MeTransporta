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
    public class AcompanhaController : ApiController
    {
        private MTContext db = new MTContext();

        #region GET ALL - OK
        [Route("api/acompanhacoleta")]
        public IEnumerable<AcompanhaColeta> GetAll()
        {
            return db.Acompanhas.ToList();
        }
        #endregion

        #region GET - IdAcompanha - OK
        [Route("api/acompanhacoleta/{idAcompanha}")]
        [HttpGet]
        public HttpResponseMessage GetAcompanha(int idAcompanha)
        {
            db.Database.Connection.Open();

            var entity = db.Acompanhas.FirstOrDefault(e => e.IdAcompanha == idAcompanha);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Orcamento com o Id = "
                        + idAcompanha.ToString() + " não foi encontrado.");
            }
        }
        #endregion

        #region GET - IdCliente - OK
        [Route("api/acompanhacoleta/cliente/{idCliente}")]
        [HttpGet]
        public HttpResponseMessage GetAcompanha_Cliente(int idCliente)
        {
            db.Database.Connection.Open();

            var entity = db.Acompanhas.FirstOrDefault(e => e.IdCliente == idCliente);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Orcamento com o Id = "
                        + idCliente.ToString() + " não foi encontrado.");
            }
        }
        #endregion

        #region GET - IdOrca - OK
        [Route("api/acompanhacoleta/orcamento/{idOrca}")]
        [HttpGet]
        public HttpResponseMessage GetAcompanha_Orca(int idOrca)
        {
            db.Database.Connection.Open();

            var entity = db.Acompanhas.FirstOrDefault(e => e.IdOrcamento == idOrca);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Orcamento com o Id = "
                        + idOrca.ToString() + " não foi encontrado.");
            }
        }
        #endregion

        #region GET - IdColeta - OK
        [Route("api/acompanhacoleta/coleta/{idColeta}")]
        [HttpGet]
        public HttpResponseMessage GetAcompanha_Coleta(int idColeta)
        {
            db.Database.Connection.Open();

            var entity = db.Acompanhas.FirstOrDefault(e => e.IdColeta == idColeta);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Orcamento com o Id = "
                        + idColeta.ToString() + " não foi encontrado.");
            }
        }
        #endregion

        #region INSERT - OK

        [Route("api/acompanhacoleta")]
        [HttpPost]
        [ResponseType(typeof(AcompanhaColeta))]
        public IHttpActionResult Post([FromBody]AcompanhaColeta acompanha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Acompanhas.Add(acompanha);
            db.SaveChanges();

            return CreatedAtRoute("Default", new { id = acompanha.IdAcompanha }, acompanha);
        }
        #endregion
        
        #region DELETE - OK
        
        [Route("api/acompanhacoleta/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entity = db.Acompanhas.FirstOrDefault(e => e.IdAcompanha == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Objeto com o ID = "
                        + id.ToString() + " não encontrado para deletar.");
                }
                else
                {
                    db.Acompanhas.Remove(entity);
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

        [Route("api/acompanhacoleta")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]AcompanhaColeta acompanha)
        {
            var entity = db.Acompanhas.FirstOrDefault(e => e.IdAcompanha == acompanha.IdAcompanha);

            if (entity != null)
            {
                #region Atributos
                
                entity.IdColeta      = acompanha.IdColeta;
                entity.IdOrcamento   = acompanha.IdOrcamento;
                entity.IdCliente     = acompanha.IdCliente;
                entity.IdMotorista   = acompanha.IdMotorista;
                entity.DataHora      = acompanha.DataHora;
                entity.IdStatus      = acompanha.IdStatus;
                entity.StatusDesc    = acompanha.StatusDesc;

                #endregion

                db.SaveChanges();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, entity));
            }
            else
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound,
                    "Objeto não localizado para alteração."));
        }
        #endregion


    }
}