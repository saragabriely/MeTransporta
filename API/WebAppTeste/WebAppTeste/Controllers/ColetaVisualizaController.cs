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
    public class ColetaVisualizaController : ApiController 
    {
        private MTContext db = new MTContext();

        #region GET ALL - OK
        [Route("api/coletavisualiza")]
        public IEnumerable<ColetaVisualiza> GetAll()
        {
            return db.ColetasVisualiza.ToList();
        }
        #endregion

        #region GET - ID - OK
        [Route("api/coletavisualiza/{id}")]
        [HttpGet]
        public HttpResponseMessage GetVisualizacao(int id)
        {
            db.Database.Connection.Open();

            var entity = db.ColetasVisualiza.FirstOrDefault(e => e.IdVisualiza == id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Visualização com o Id = "
                        + id.ToString() + " não foi encontrada.");
            }
        }
        #endregion
        
        #region INSERT - OK

        [Route("api/coletavisualiza")]
        [HttpPost]
        [ResponseType(typeof(ColetaVisualiza))]
        public IHttpActionResult Post([FromBody]ColetaVisualiza visualizacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ColetasVisualiza.Add(visualizacao);
            db.SaveChanges();

            return CreatedAtRoute("Default", new { id = visualizacao.IdVisualiza }, visualizacao);
        }
        #endregion
        
    }
}