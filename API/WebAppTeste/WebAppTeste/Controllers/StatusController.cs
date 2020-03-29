using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAppTeste.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Utilities;
using System.Web.Http.Description;

namespace WebAppTeste.Controllers
{
    public class StatusController : ApiController
    {
        private MTContext db = new MTContext();

        #region GETALL - OK
        [Route("api/status")]
        public IEnumerable<Status> GetAll()
        {
            using (MTContext db = new MTContext())
            {
                return db.Status.ToList();
            }
        }

        #endregion

        #region GET - ID - OK
        [Route("api/status/{id}")]
        [HttpGet]
        public HttpResponseMessage GetStatus(int id)
        {
            using (MTContext entities = new MTContext())
            {
                entities.Database.Connection.Open();

                var entity = entities.Status.FirstOrDefault(e => e.IdStatus == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Status com o Id = " 
                            + id.ToString() + " não encontrado.");
                }
            }
        }
        #endregion

        #region GET - DESCRICAO
        [Route("api/status/busca")]
        [HttpGet]
        public HttpResponseMessage GetStatus(string status)
        {
            using (MTContext entities = new MTContext())
            {
                entities.Database.Connection.Open();

                var entity = entities.Status.FirstOrDefault(e => e.DescricaoStatus == status);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity, "Status deletado com sucesso");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Status '" 
                            + status.ToString() + "' não encontrado.");
                }
            }
        }
        #endregion

        #region INSERT - OK
        [Route("api/status")]
        [HttpPost]
        [ResponseType(typeof(Status))]
        public IHttpActionResult Post([FromBody]Status status)
        {
            using (MTContext entities = new MTContext())
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                entities.Status.Add(status);
                entities.SaveChanges();

                return CreatedAtRoute("Default", new { id = status.IdStatus }, status);
            }
        }
        #endregion
        
        #region DELETE - OK
        
        [Route("api/status/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (MTContext entities = new MTContext())
                {
                    var entity = entities.Status.FirstOrDefault(e => e.IdStatus == id);

                    if (entity != null)
                    {
                        entities.Status.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Status com o ID = " + id.ToString() + " não encontrado para deletar.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        #endregion

        #region UPDATE 

        [Route("api/status")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]Status status)
        {
            var entity = db.Status.FirstOrDefault(e => e.IdStatus == status.IdStatus);

            if (entity != null)
            {
                // entity.IdCliente = cliente.IdCliente;
                entity.IdStatus         = status.IdStatus;
                entity.DescricaoStatus  = status.DescricaoStatus;
                
                db.SaveChanges();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, entity));
            }
            else
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound,
                    "Status não localizado para alteração."));

        }
        #endregion

        #region INSERT - Comentados
        /*
         //POST - INSERT 
        //[Route("")]
        [HttpPost]
        public HttpResponseMessage Insert([FromBody]Status status)
        {
            try
            {  //  new SuspendableSqlAzureExecutionStrategy().Execute(() =>
               // {
                    using (MTContext entities = new MTContext())
                    {
                        entities.Status.Add(status);
                        entities.SaveChanges();

                        var message = Request.CreateResponse(HttpStatusCode.Created, status);

                       // message.Headers.Location = new Uri(Request.RequestUri + status.IdStatus.ToString());

                        return Request.CreateResponse(HttpStatusCode.Accepted, message);
                    }
              //  });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            } 
        }
         */

        #endregion

        #region COMENTARIOS
        /*
        //POST - INSERT 
        //[Route("")]
        [HttpPost]
        public HttpResponseMessage Insert([FromBody]Status status)
        {
            try
            {  //  new SuspendableSqlAzureExecutionStrategy().Execute(() =>
               // {
                    using (MTContext entities = new MTContext())
                    {
                        entities.Status.Add(status);
                        entities.SaveChanges();

                        var message = Request.CreateResponse(HttpStatusCode.Created, status);

                       // message.Headers.Location = new Uri(Request.RequestUri + status.IdStatus.ToString());

                        return Request.CreateResponse(HttpStatusCode.Accepted, message);
                    }
              //  });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            } 
        }
        */
        #endregion
    }
}