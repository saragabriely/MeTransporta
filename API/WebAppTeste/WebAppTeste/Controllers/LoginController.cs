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
    //[RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        private MTContext db = new MTContext();

        #region GET ALL
        [Route("api/login")]
        public IEnumerable<Login> GetAll()
        {
            using (MTContext db = new MTContext())
            {
                return db.Acesso.ToList();
            }
        }
        #endregion

        #region GET ID 
        [Route("api/login/{idLogin}")]
        [HttpGet]
        public HttpResponseMessage GetLogin(int idLogin)
        {
            using (MTContext entities = new MTContext())
            {
                entities.Database.Connection.Open();

                var entity = entities.Acesso.FirstOrDefault(e => e.IdLogin == idLogin);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Login com o Id = " 
                        + idLogin.ToString() + " não encontrado.");
                }
            }
        }
        #endregion

        #region GET ID 
        [Route("api/login/cliente/{idCliente}")]
        [HttpGet]
        public HttpResponseMessage GetLoginCliente(int idCliente)
        {
            using (MTContext entities = new MTContext())
            {
                entities.Database.Connection.Open();

                var entity = entities.Acesso.FirstOrDefault(e => e.IdCliente == idCliente);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Login com o IdCliente = "
                        + idCliente.ToString() + " não encontrado.");
                }
            }
        }
        #endregion

        #region CPF 
        [Route("api/login/cpf")]
        [HttpGet]
        public HttpResponseMessage GetLogin(string cpf)
        {
            using (MTContext entities = new MTContext())
            {
                entities.Database.Connection.Open();

                var entity = entities.Acesso.FirstOrDefault(e => e.Ccpf == cpf);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Usuário com o CPF = " + cpf.ToString()
                        + " não encontrado.");
                }
            }
        }
        #endregion

        #region E-mail e Senha
        [Route("api/login/loga/")]
        [HttpGet]
        public HttpResponseMessage GetLoginTeste(string email, string senha)
        {
            using (MTContext entities = new MTContext())
            {
                entities.Database.Connection.Open();

                var entity = entities.Acesso.FirstOrDefault(e => e.Email == email);

                if (entity != null)
                {
                    
                    if(entity.Senha == senha)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                      return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Senha incorreta.");
                       //return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Email não encontrado.");
                }
            }
        }
        #endregion

        #region UPDATE 
        [Route("api/login")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]Login login)
        {
            var entity = db.Acesso.FirstOrDefault(e => e.IdLogin == login.IdLogin);

            if (entity != null)
            {
                // entity.IdLogin       = login.IdLogin;
                entity.Ccpf          = login.Ccpf;
                entity.IdCliente     = login.IdCliente;
                entity.Email         = login.Email;
                entity.Senha         = login.Senha;
                entity.IdTipoUsuario = login.IdTipoUsuario;
                entity.IdStatus      = login.IdStatus;
                db.SaveChanges();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, entity));
            }
            else
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound,
                    "Teste não localizado para alteração."));
        }
        #endregion
        

        #region Comentado
        /*
         * #region E-mail e Senha
        [Route("api/login/logar")]
        [HttpGet]
        public HttpResponseMessage GetLoginTeste(string email)
        {
            using (MTContext entities = new MTContext())
            {
                entities.Database.Connection.Open();

                var entity = entities.Acesso.FirstOrDefault(e => e.Email == email);

                //var entity2 = entities.Acesso.Fi 

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity.Email + " - " + entity.Senha);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Email não encontrado.");
                }
            }
        }
        #endregion
         * 
         * 
         * 
        public HttpResponseMessage GetLoginTeste(string email)
        {
            using (MTContext entities = new MTContext())
            {
                entities.Database.Connection.Open();

                var entity = entities.Acesso.FirstOrDefault(e => e.Email == email);

                //var entity2 = entities.Acesso.Fi 

                if (entity != null)
                {
                    /*
                    if(entity.Senha == senha)
                    {* /

                    // var entit = entities.Acesso.FirstOrDefault(e => e.IdLogin == entity.IdLogin);

                    //   return Request.CreateResponse(HttpStatusCode.OK, entit);

                    return Request.CreateResponse(HttpStatusCode.OK, email);

                    /*}
                    else
                    {
                      return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Senha incorreta.");
                   // return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }* /
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Email não encontrado.");
                }
            }
        } */
        #endregion
    }
}