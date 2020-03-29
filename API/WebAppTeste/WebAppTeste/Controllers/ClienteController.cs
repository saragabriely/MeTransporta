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
    public class ClienteController : ApiController 
    {
        private MTContext db = new MTContext();

        #region GET ALL - OK
        [Route("api/cliente")]
        public IEnumerable<Cliente> GetAll()
        {        
           return db.Clientes.ToList();
        }
        #endregion

        #region GET - ID - OK
        [Route("api/cliente/{id}")]
        [HttpGet]
        public HttpResponseMessage GetCliente(int id)
        {
            db.Database.Connection.Open();

            var entity = db.Clientes.FirstOrDefault(e => e.IdCliente == id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cliente com o Id = " 
                    + id.ToString() + " não encontrado.");
            }            
        }
        #endregion

        #region GET - ID - OK
        [Route("api/cliente/tipousuario/{id}")]
        [HttpGet]
        public HttpResponseMessage GetClienteTipo(int id)
        {
            db.Database.Connection.Open();

            TipoUsuarioController tipoControl = new TipoUsuarioController();
            
            var entity = db.Clientes.FirstOrDefault(e => e.IdTipoUsuario == id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cliente do tipo com o id = "
                    + id.ToString() + " não encontrado.");
            }
        }
        #endregion

        #region GET - CPF
        [Route("api/cliente/cpf")]
        [HttpGet]
        public HttpResponseMessage GetClienteCpf(string cpf)
        {
            db.Database.Connection.Open();

            var entity = db.Clientes.FirstOrDefault(e => e.Ccpf == cpf);

            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cliente com o '"+ cpf.ToString()
                    + "' Cpf não encontrado.");
            }
        }
        #endregion
        
        #region INSERT - OK
        
        [Route("api/cliente")]
        [HttpPost]
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult Post([FromBody]Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clientes.Add(cliente);
            db.SaveChanges();

            return CreatedAtRoute("Default", new { id = cliente.IdCliente }, cliente);
        }
    
        #endregion

        #region DELETE - OK
        
        [Route("api/cliente/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entity = db.Clientes.FirstOrDefault(e => e.IdCliente == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, 
                        "Cliente com o ID = " + id.ToString() + " não encontrado para deletar.");
                }
                else
                {
                    db.Clientes.Remove(entity);
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
        [Route("api/cliente")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]Cliente cliente)
    {
            var entity = db.Clientes.FirstOrDefault(e => e.IdCliente == cliente.IdCliente);

            if (entity != null)
            {
                #region Atributos - Cliente
                
                // entity.IdCliente = cliente.IdCliente;
                entity.Cnome         = cliente.Cnome;
                entity.Crg           = cliente.Crg;
                entity.Ccpf          = cliente.Ccpf;
                entity.Csexo         = cliente.Csexo;
                entity.CdataNascto   = cliente.CdataNascto;
                entity.Ccelular      = cliente.Ccelular;
                entity.Ccelular2     = cliente.Ccelular2;
                entity.Cendereco     = cliente.Cendereco;
                entity.Cnumero       = cliente.Cnumero;
                entity.Ccomplemento  = cliente.Ccomplemento;
                entity.Cbairro       = cliente.Cbairro;
                entity.Ccidade       = cliente.Ccidade;
                entity.Ccep          = cliente.Ccep;
                entity.Cuf           = cliente.Cuf;
                entity.Cemail        = cliente.Cemail;
                entity.IdTipoUsuario = cliente.IdTipoUsuario;
                entity.IdStatus      = cliente.IdStatus;
                entity.Csenha        = cliente.Csenha;

                #endregion

                db.SaveChanges();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, entity));
            }
            else
            {
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound,
                    "Cliente/cadastro não localizado para alteração."));
            }
        }
        #endregion

        #region INSERT - COMENTADO
        /*
        //POST - INSERT 
        //[Route("")]
        [HttpPost]
        public HttpResponseMessage Insert([FromBody]Cliente cliente)
        {
            try
            {
                using (MTContext entities = new MTContext())
                {
                    entities.Clientes.Add(cliente);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, cliente);

                    message.Headers.Location = new Uri(Request.RequestUri + cliente.IdCliente.ToString());

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