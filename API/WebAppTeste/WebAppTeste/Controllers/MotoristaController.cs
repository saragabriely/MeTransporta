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
    public class MotoristaController : ApiController
    {
        private MTContext db = new MTContext();

        #region GET ALL - OK
        [Route("api/motorista")]
        public IEnumerable<Motorista> GetAll()
        {
            using (MTContext db = new MTContext())
            {
                return db.Motoristas.ToList();
            }
        }
        #endregion

        #region GET - ID - Motorista
        [Route("api/motorista/{id}")]
        [HttpGet]
        public HttpResponseMessage GetMotorista(int id)
        {
            using (MTContext entities = new MTContext())
            {
                entities.Database.Connection.Open();

                var entity = entities.Motoristas.FirstOrDefault(e => e.IdMotorista == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Motorista com o Id = " 
                        + id.ToString() + " nao encontrado.");
                }
            }
        }
        #endregion

        #region GET - ID - IdCliente
        [Route("api/motorista/cliente/{id}")]
        [HttpGet]
        public HttpResponseMessage GetMotoristaCliente(int id)
        {
            using (MTContext entities = new MTContext())
            {
                entities.Database.Connection.Open();

                var entity = entities.Motoristas.FirstOrDefault(e => e.IdCliente == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Motorista com o Id = "
                        + id.ToString() + " nao encontrado.");
                }
            }
        }
        #endregion
        
        #region GET - CPF
        [Route("api/motorista/busca")]
        [HttpGet]
        public HttpResponseMessage GetMotorista(string cpf)
        {
            db.Database.Connection.Open();

            var entity = db.Motoristas.FirstOrDefault(e => e.Ccpf == cpf);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Motorista com o CPF = "
                    + cpf.ToString() + " nao encontrado.");
            }
            
        }
        #endregion
        
        #region INSERT - OK

        [Route("api/motorista")]
        [HttpPost]
        [ResponseType(typeof(Motorista))]
        public IHttpActionResult Post([FromBody]Motorista motorista)
        {
            using (MTContext entities = new MTContext())
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                entities.Motoristas.Add(motorista);
                entities.SaveChanges();

                return CreatedAtRoute("Default", new { id = motorista.IdMotorista }, motorista);
            }
        }
        #endregion

        #region DELETE - OK 
        [Route("api/motorista/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (MTContext entities = new MTContext())
                {
                    var entity = entities.Motoristas.FirstOrDefault(e => e.IdMotorista == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Motorista com o ID = " + id.ToString() + " não encontrado para deletar.");
                    }
                    else
                    {
                        entities.Motoristas.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
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
        [Route("api/motorista")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]Motorista motorista)
        {
            var entity = db.Motoristas.FirstOrDefault(e => e.IdMotorista == motorista.IdMotorista);

            if (entity != null)
            {
                entity.IdCliente     = motorista.IdCliente;
                entity.IdMotorista   = motorista.IdMotorista;
                entity.MnumeroCnh    = motorista.MnumeroCnh;
                entity.McategoriaCnh = motorista.McategoriaCnh;
                entity.MvalidadeCnh  = motorista.MvalidadeCnh;
                entity.IdStatus      = motorista.IdStatus;

                db.SaveChanges();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, entity));
            }
            else
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound,
                    "Teste não localizado para alteração."));

        }
        #endregion


        #region COMENTÁRIOS

        /*
         public HttpResponseMessage Insert([FromBody]Motorista motorista)
        {
            try
            {
                using (MTContext entities = new MTContext())
                {
                    entities.Motoristas.Add(motorista);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, motorista);

                    message.Headers.Location = new Uri(Request.RequestUri + motorista.IdMotorista.ToString());

                    return Request.CreateResponse(HttpStatusCode.Accepted, message);
                }
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