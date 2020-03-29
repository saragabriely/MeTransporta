using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAppTeste.Models;

namespace WebAppTeste.Controllers
{
    public class TipoUsuarioController : ApiController
    {
        private MTContext db = new MTContext();

        public HttpClient client = new HttpClient();

        //private ITesteRepository _service;// = new ITesteRepository();

        #region GETALL - OK
        [Route("api/tipousuario")]
        public IEnumerable<TipoUsuario> GetAll()
        {
            if (db != null)
            {
                return db.TipoUsuarios.ToList();
            }
            return null;
        }
        #endregion

        #region GET - ID - OK
        [Route("api/tipousuario/{id}")]
        [HttpGet]
        public HttpResponseMessage GetTeste(int id)
        {
            var entity = db.TipoUsuarios.FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tipo de usuários com o Id = "
                         + id.ToString() + " não encontrado.");
            }
        }
        #endregion

        #region GET - STRING 
        [Route("api/tipousuario/desc")]
        [HttpGet]
        public HttpResponseMessage GetTeste(string desc)
        {
            var entity = db.Testes.FirstOrDefault(e => e.descricao == desc);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Teste com a descrição = "
                         + desc.ToString() + " não encontrado.");
            }
        }
        #endregion

        /*
        #region INSERT - OK
        //POST - INSERT 
        //[Route("")]
        [HttpPost]
        [ResponseType(typeof(teste))]
        public IHttpActionResult Post([FromBody]teste Teste)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Testes.Add(Teste);
            db.SaveChanges();

            return CreatedAtRoute("Default", new { id = Teste.IdTeste }, Teste);
        }
        #endregion

        #region DELETE - OK
        // DELETE 
        //[Route("")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entity = db.Testes.FirstOrDefault(e => e.IdTeste == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Teste com o ID = " + id.ToString() + " não encontrado para deletar.");
                }
                else
                {
                    db.Testes.Remove(entity);
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
        [HttpPut]
        public IHttpActionResult Put([FromBody]TipoUsuario objeto)
        {
            var entity = db.Testes.FirstOrDefault(l => l.IdTeste == objeto.IdTeste);

            if (entity != null)
            {
                entity.IdTeste = objeto.IdTeste;
                entity.descricao = objeto.descricao;
                db.SaveChanges();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, entity));
            }
            else
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound,
                    "Teste não localizado para alteração."));
        }
        #endregion

        */


    }
}