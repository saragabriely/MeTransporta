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
    public class ContaBancariaController : ApiController
    {
        private MTContext db = new MTContext();

        #region GET ALL - OK
        [Route("api/contabancaria")]
        public IEnumerable<ContaBancaria> GetAll()
        {
            using (MTContext db = new MTContext())
            {
                return db.ContaBancarias.ToList();
            }
        }
        #endregion

        #region GET - ID - OK
        [Route("api/contabancaria/{id}")]
        [HttpGet]
        public HttpResponseMessage GetContaBancaria(int id)
        {
            db.Database.Connection.Open();
            
            var entity = db.ContaBancarias.FirstOrDefault(e => e.IdContaBancaria == id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Conta bancária com o Id = " 
                        + id.ToString() + " não foi encontrada.");
            }
        }
        #endregion

        #region INSERT - OK
        
        [Route("api/contabancaria")]
        [HttpPost]
        [ResponseType(typeof(ContaBancaria))]
        public IHttpActionResult Post([FromBody]ContaBancaria conta)
        {
            using (MTContext entities = new MTContext())
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                entities.ContaBancarias.Add(conta);
                entities.SaveChanges();

                return CreatedAtRoute("Default", new { id = conta.IdContaBancaria }, conta);
            }
        }
        #endregion

        #region DELETE - OK
        // DELETE 
        [Route("api/contabancaria/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (MTContext entities = new MTContext())
                {
                    var entity = entities.ContaBancarias.FirstOrDefault(e => e.IdContaBancaria == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Conta bancária com o ID = " 
                            + id.ToString() + " não encontrado para deletar.");
                    }
                    else
                    {
                        entities.ContaBancarias.Remove(entity);
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
        [Route("api/contabancaria")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]ContaBancaria contaBancaria)
        {
            var entity = db.ContaBancarias.FirstOrDefault(e => e.IdContaBancaria == contaBancaria.IdContaBancaria);

            if (entity != null)
            {
                entity.IdBanco            = contaBancaria.IdBanco;
                entity.MAgencia           = contaBancaria.MAgencia;
                entity.MDigAgencia        = contaBancaria.MDigAgencia;
                entity.MConta             = contaBancaria.MConta;
                entity.MDigConta          = contaBancaria.MDigConta;
                entity.IdTipoConta        = contaBancaria.IdTipoConta;
                entity.MUltimaAtualizacao = contaBancaria.MUltimaAtualizacao;
                entity.IdStatus           = contaBancaria.IdStatus;
                entity.MDataCadastro      = contaBancaria.MDataCadastro;
                entity.IdStatus           = contaBancaria.IdStatus;
                entity.Ccpf               = contaBancaria.Ccpf;

                db.SaveChanges();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, entity));
            }
            else
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound,
                    "Teste não localizado para alteração."));
        }
        #endregion
        

        #region INSERT - COMENTADO
        /*
        //POST - INSERT 
        //[Route("")]
        [HttpPost]
        public HttpResponseMessage Insert([FromBody]ContaBancaria contaBancaria)
        {
            try
            {
                using (MTContext entities = new MTContext())
                {
                    entities.ContaBancarias.Add(contaBancaria);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, contaBancaria);

                    message.Headers.Location = new Uri(Request.RequestUri + contaBancaria.IdContaBancaria.ToString());

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