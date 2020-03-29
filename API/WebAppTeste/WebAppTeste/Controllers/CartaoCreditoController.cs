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
    public class CartaoCreditoController : ApiController
    {
        private MTContext db = new MTContext();

        #region GET ALL - OK
        [Route("api/cartaocredito")]
        public IEnumerable<CartaoCredito> GetAll()
        {
            return db.Cartao.ToList();
        }
        #endregion

        #region GET - ID 
        [Route("api/cartaocredito/{id}")]
        [HttpGet]
        public HttpResponseMessage GetCartao(int id)
        {
            db.Database.Connection.Open();

            var entity = db.Cartao.FirstOrDefault(e => e.IdCartao == id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cartao de crédito com o Id = " 
                        + id.ToString() + " não encontrado!");
            }
        }
        #endregion

        #region GET - CPF
        [Route("api/cartaocredito/cpf")]
        [HttpGet]
        public HttpResponseMessage GetCartaoCpf(string cpf)
        {
            using (MTContext entities = new MTContext())
            {
                entities.Database.Connection.Open();

                var entity = entities.Cartao.FirstOrDefault(e => e.Ccpf == cpf);
                
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cpf não encontrado.");
                }
            }
        }
        #endregion
        
        #region INSERT -OK

        [Route("api/cartaocredito")]
        [HttpPost]
        [ResponseType(typeof(CartaoCredito))]
        public IHttpActionResult Post([FromBody]CartaoCredito cartao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cartao.Add(cartao);
            db.SaveChanges();

            return CreatedAtRoute("Default", new { id = cartao.IdCartao }, cartao);
        }
        #endregion

        #region DELETE - OK
        // DELETE 
        [Route("api/cartaocredito/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entity = db.Cartao.FirstOrDefault(e => e.IdCartao == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cartão de crédito com o ID '" 
                         + id.ToString() + "' não foi encontrado para deletar.");
                }
                else
                {
                    db.Cartao.Remove(entity);
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
        [Route("api/cartaocredito")] 
        [HttpPut]
        public IHttpActionResult Put([FromBody]CartaoCredito objeto)
        {
            var entity = db.Cartao.FirstOrDefault(l => l.IdCartao == objeto.IdCartao);

            if (entity != null)
            {
                entity.IdCartao           = objeto.IdCartao;
                entity.IdCliente          = objeto.IdCliente;
                entity.Ccpf               = objeto.Ccpf;
                entity.CNumeroCartao      = objeto.CNumeroCartao;
                entity.IdBandeira         = objeto.IdBandeira;
                entity.CDataValidade      = objeto.CDataValidade;
                entity.CCodigoSeg         = objeto.CCodigoSeg;
                entity.IdStatus           = objeto.IdStatus;
                entity.CDataInativacao    = objeto.CDataInativacao;
                entity.CUltimaAtualizacao = objeto.CUltimaAtualizacao;

                db.SaveChanges();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, entity));
            }
            else
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound,
                    "Teste não localizado para alteração."));
        }

        /*
        public HttpResponseMessage Update(CartaoCredito cartao, [FromBody] int id)
        {
            try
            {
                using (MTContext entities = new MTContext())
                {
                    var entity = entities.Cartao.FirstOrDefault(e => e.IdCartao == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cartão de crédito com o Id '" 
                            + id.ToString() + "' não foi encontrado para atualizar.");
                    }
                    else
                    {
                        //entity.IdCartao = cartao.IdCartao;
                        //entity.IdCliente = cartao.IdCliente;
                        //entity.Ccpf = cartao.Ccpf;
                        entity.CNumeroCartao = cartao.CNumeroCartao;
                        entity.IdBandeira = cartao.IdBandeira;
                        entity.CDataValidade = cartao.CDataValidade;
                        entity.CCodigoSeg = cartao.CCodigoSeg;
                        entity.IdStatus = cartao.IdStatus;
                        //entity.CDataInativacao = cartao.CDataInativacao;
                        //entity.CUltimaAtualizacao = cartao.CUltimaAtualizacao;
                        
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }*/
        #endregion

        #region INSERT - COMENTADO

        /*
         [HttpPut]
        public HttpResponseMessage Update(CartaoCredito cartao, [FromBody] int id)
        {
            try
            {
                using (MTContext entities = new MTContext())
                {
                    var entity = entities.Cartao.FirstOrDefault(e => e.IdCartao == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cartão de crédito com o Id '" 
                            + id.ToString() + "' não foi encontrado para atualizar.");
                    }
                    else
                    {
                        //entity.IdCartao = cartao.IdCartao;
                        //entity.IdCliente = cartao.IdCliente;
                        //entity.Ccpf = cartao.Ccpf;
                        entity.CNumeroCartao = cartao.CNumeroCartao;
                        entity.IdBandeira = cartao.IdBandeira;
                        entity.CDataValidade = cartao.CDataValidade;
                        entity.CCodigoSeg = cartao.CCodigoSeg;
                        entity.IdStatus = cartao.IdStatus;
                        //entity.CDataInativacao = cartao.CDataInativacao;
                        //entity.CUltimaAtualizacao = cartao.CUltimaAtualizacao;
                        
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
         */

        /*
         //POST - INSERT 
        //[Route("")]
        [HttpPost]
        public HttpResponseMessage Insert([FromBody]CartaoCredito cartao)
        {
            try
            {
                using (MTContext entities = new MTContext())
                {
                    entities.Cartao.Add(cartao);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, cartao);

                    message.Headers.Location = new Uri(Request.RequestUri + cartao.IdCartao.ToString());

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