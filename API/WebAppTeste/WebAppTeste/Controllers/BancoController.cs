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
    public class BancoController : ApiController
    {
        private MTContext db = new MTContext();

        #region GETALL
        [Route("api/bancos")]
        public IEnumerable<Banco> GetAll()
        {
            return db.Bancos.ToList();
        }
        #endregion

        #region GET - ID - OK
        [Route("api/bancos/{id}")]
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var entity = db.Bancos.FirstOrDefault(e => e.IdBanco == id);

            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Banco com o Id = " 
                        + ToString() + " não encontrado.");
            }
        }
        #endregion
        
        #region INSERT - OK
        
        [Route("api/bancos")]
        [HttpPost]
        [ResponseType(typeof(Banco))]
        public IHttpActionResult Post([FromBody]Banco banco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bancos.Add(banco);
            db.SaveChanges();

            return CreatedAtRoute("Default", new { id = banco.IdBanco }, banco);
        }

        #endregion

        #region DELETE - OK
        
        [Route("api/bancos/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entity = db.Bancos.FirstOrDefault(e => e.IdBanco == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Banco com o ID = " + id.ToString() + " não encontrado para deletar.");
                }
                else
                {
                    db.Bancos.Remove(entity);
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
        
    }
}