using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppTeste.Models;
using HttpPostAttribute   = System.Web.Mvc.HttpPostAttribute;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute    = System.Web.Http.HttpGetAttribute;
using HttpPutAttribute    = System.Web.Http.HttpPutAttribute;
using WebAppTeste.Interface;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Text;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace WebAppTeste.Controllers
{
    public class BandeiraController : ApiController
    {
        private MTContext db = new MTContext();

        public HttpClient client = new HttpClient();

        //private ITesteRepository _service;// = new ITesteRepository();

        #region GETALL - OK
        [Route("api/bandeira")]
        public IEnumerable<BandeiraCartao> GetAll()
        {
            if (db != null)
            {
                return db.Bandeiras.ToList();
            }
            return null;
        }

        #endregion

        #region GET - ID - OK
        [Route("api/bandeira/{id}")]
        [HttpGet]
        public HttpResponseMessage GetBandeira(int id)
        {
            var entity = db.Bandeiras.FirstOrDefault(e => e.IdBandeira == id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Bandeira com o Id = "
                         + id.ToString() + " não encontrada.");
            }
        }
        #endregion

        #region INSERT - OK
        [Route("api/bandeira")]
        [HttpPost]
        [ResponseType(typeof(BandeiraCartao))]
        public IHttpActionResult Post([FromBody]BandeiraCartao bandeira)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bandeiras.Add(bandeira);
            db.SaveChanges();

            return CreatedAtRoute("Default", new { id = bandeira.IdBandeira }, bandeira);
        }
        #endregion

        #region DELETE - OK
        [Route("api/bandeira/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entity = db.Bandeiras.FirstOrDefault(e => e.IdBandeira == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, 
                        "Bandeira com o ID = " + id.ToString() + " não encontrada para deletar.");
                }
                else
                {
                    db.Bandeiras.Remove(entity);
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

        #region UPDATE - OK
        [Route("api/bandeira")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]BandeiraCartao objeto)
        {
            var entity = db.Bandeiras.FirstOrDefault(l => l.IdBandeira == objeto.IdBandeira);

            if (entity != null)
            {
                entity.IdBandeira = objeto.IdBandeira;
                entity.Digito     = objeto.Digito;
                entity.Descricao  = objeto.Descricao;
                db.SaveChanges();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, entity));
            }
            else
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound,
                    "Bandeira não localizada para alteração."));
        }
        #endregion
       
    }
}
