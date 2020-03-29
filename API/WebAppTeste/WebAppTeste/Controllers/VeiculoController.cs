using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebAppTeste.Models;

namespace WebAppTeste.Controllers
{
    public class VeiculoController : ApiController
    {
        private MTContext db = new MTContext();

        #region GET ALL - OK
        [Route("api/veiculo")]
        public IEnumerable<Veiculo> GetAll()
        {
            using (MTContext db = new MTContext())
            {
                return db.Veiculos.ToList();
            }
        }
        #endregion

        #region GET ALL - OK
        [Route("api/veiculo/motorista/{idMotorista}")]
        [HttpGet]
        public HttpResponseMessage GetVeiculoMotorista(int idMotorista)
        {
            db.Database.Connection.Open();

            var entity = db.Veiculos.FirstOrDefault(e => e.IdMotorista == idMotorista);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Veiculo(s) do motorista com o Id '"
                    + idMotorista.ToString() + "' não encontrado.");
            }
            
        }
        #endregion

        #region GET - ID - OK
        [Route("api/veiculo/{id}")]
        [HttpGet]
        public HttpResponseMessage GetVeiculo(int id)
        {
            using (MTContext entities = new MTContext())
            {
                entities.Database.Connection.Open();

                var entity = entities.Veiculos.FirstOrDefault(e => e.IdVeiculo == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Veiculo com o Id '" 
                        + id.ToString() + "' não encontrado.");
                }
            }
        }
        #endregion

        #region GET - Placa
        [Route("api/veiculo/placa")]
        [HttpGet]
        public HttpResponseMessage GetPlaca(string placa)
        {
            db.Database.Connection.Open();

            var entity = db.Veiculos.FirstOrDefault(e => e.Placa == placa);

            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Veiculo com o '" + placa.ToString()
                    + "' não encontrado.");
            }
        }
        #endregion

        #region INSERT - OK

        [Route("api/veiculo")]
        [HttpPost]
        [ResponseType(typeof(Veiculo))]
        public IHttpActionResult Post([FromBody]Veiculo veiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Veiculos.Add(veiculo);
            db.SaveChanges();

            return CreatedAtRoute("Default", new { id = veiculo.IdVeiculo }, veiculo);
        }        
        #endregion

        #region DELETE - OK
        
        [Route("api/veiculo/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (MTContext entities = new MTContext())
                {
                    var entity = entities.Veiculos.FirstOrDefault(e => e.IdVeiculo == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Veiculo com o ID = " + id.ToString() + " não encontrado para deletar.");
                    }
                    else
                    {
                        entities.Veiculos.Remove(entity);
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
        [Route("api/veiculo")]
        [HttpPut]
        public IHttpActionResult Put([FromBody] Veiculo veiculo)
        {
            var entity = db.Veiculos.FirstOrDefault(e => e.IdVeiculo == veiculo.IdVeiculo);
            
            if (entity != null)
            {
                #region Parametros

               // entity.IdVeiculo             = veiculo.IdVeiculo;
               // entity.IdMotorista           = veiculo.IdMotorista;
                entity.Placa                 = veiculo.Placa;
                entity.Modelo                = veiculo.Modelo;
                entity.Marca                 = veiculo.Marca;
                entity.Renavam               = veiculo.Renavam;
                entity.Chassi                = veiculo.Chassi;
                entity.AnoFabricacao         = veiculo.AnoFabricacao;
                entity.IdTipoVeiculo         = veiculo.IdTipoVeiculo;
                entity.CarroceriaAltura      = veiculo.CarroceriaAltura;
                entity.CarroceriaLargura     = veiculo.CarroceriaLargura;
                entity.CarroceriaComprimento = veiculo.CarroceriaComprimento;
                entity.Refrigeracao          = veiculo.Refrigeracao;
                entity.CapacidadeCarga       = veiculo.CapacidadeCarga;
                entity.IdStatus              = veiculo.IdStatus;
                entity.TipoVeiculoDesc       = veiculo.TipoVeiculoDesc;
                entity.TipoCarroceriaDesc    = veiculo.TipoCarroceriaDesc;
                entity.IdTipoCarroceria      = veiculo.IdTipoCarroceria;
                entity.DescStatus            = veiculo.DescStatus;

                #endregion

                db.SaveChanges();

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, entity));
            }
            else
            {
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound,
                    "Veiculo não localizado para alteração."));
            }
           
        }
        #endregion


        #region COMENTÁRIOS 
        /*
        public HttpResponseMessage Insert([FromBody]Veiculo veiculo)
        {
            try
            {
                using (MTContext entities = new MTContext())
                {
                    entities.Veiculos.Add(veiculo);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, veiculo);

                    message.Headers.Location = new Uri(Request.RequestUri + veiculo.IdVeiculo.ToString());

                    return Request.CreateResponse(HttpStatusCode.Accepted, message);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }*/
        #endregion

    }
}