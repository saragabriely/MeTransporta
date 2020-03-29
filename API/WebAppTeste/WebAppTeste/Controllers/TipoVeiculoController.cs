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
    public class TipoVeiculoController : ApiController
    {
        private MTContext db = new MTContext();

        #region GET ALL - OK 
        [Route("api/tipoveiculo")]
        public IEnumerable<TipoVeiculo> GetAll()
        {
            using (MTContext db = new MTContext())
            {
                return db.TiposVeiculo.ToList();
            }
        }
        #endregion

        #region GET - ID 
        [Route("api/tipoveiculo/{id}")]
        [HttpGet]
        public HttpResponseMessage GetTipoVeiculo(int id)
        {
            using (MTContext entities = new MTContext())
            {
                entities.Database.Connection.Open();

                var entity = entities.TiposVeiculo.FirstOrDefault(e => e.IdTipoVeiculo == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                   return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tipo de veículo com o Id = " 
                            + id.ToString() + " não encontrado!");
                }
            }
        }
        #endregion

        #region COMENTÁRIOS
        /*
        [HttpGet]
        public void GetTipoVeiculo(string tipo)
        {
            using (MTContext entities = new MTContext())
            {
                entities.Database.Connection.Open();

                var entity = entities.TiposVeiculo.FirstOrDefault(e => e.Tipo_Veiculo == tipo);
                if (entity != null)
                {
                    Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tipo de veículo = "
                            + tipo.ToString() + " não foi encontrado!");
                }
            }
        } */
        #endregion

    }
}