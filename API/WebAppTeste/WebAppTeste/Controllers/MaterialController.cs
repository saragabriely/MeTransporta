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
    public class MaterialController : ApiController
    {
        private MTContext db = new MTContext();

        #region GET ALL - OK
        [Route("api/material")]
        public IEnumerable<Material> GetAll()
        {
            return db.Materials.ToList();
        }
        #endregion

        #region GET - ID - OK
        [Route("api/material/{id}")]
        [HttpGet]
        public HttpResponseMessage GetMaterial(int id)
        {
            db.Database.Connection.Open();

            var entity = db.Materials.FirstOrDefault(e => e.IdMaterial == id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Material com o Id = "
                        + id.ToString() + " não foi encontrado.");
            }
        }
        #endregion
        
        #region INSERT - OK

        [Route("api/material")]
        [HttpPost]
        [ResponseType(typeof(Material))]
        public IHttpActionResult Post([FromBody]Material material)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Materials.Add(material);
            db.SaveChanges();

            return CreatedAtRoute("Default", new { id = material.IdMaterial }, material);
        }
        #endregion
        
        #region DELETE - OK
        
        [Route("api/material/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entity = db.Materials.FirstOrDefault(e => e.IdMaterial == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Material com o ID = "
                        + id.ToString() + " não encontrado para deletar.");
                }
                else
                {
                    db.Materials.Remove(entity);
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
        [Route("api/material")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]Material material)
        {
            var entity = db.Materials.FirstOrDefault(e => e.IdMaterial == material.IdMaterial);

            if (entity != null)
            {
                #region Atributos

                entity.IdColeta          = material.IdColeta;
                entity.IdCliente         = material.IdCliente;
                entity.MatTipo           = material.MatTipo;
                entity.MatFragilidade    = material.MatFragilidade;
                entity.MatDescricao      = material.MatDescricao;
                entity.MatPeso           = material.MatPeso;
                entity.MatVolume         = material.MatVolume;
                entity.MatAltura         = material.MatAltura;
                entity.MatLargura        = material.MatLargura;
                entity.DataCadastro      = material.DataCadastro;
                entity.UltimaAtualizacao = material.UltimaAtualizacao;
                entity.IdStatus          = material.IdStatus;

                #endregion

                db.SaveChanges();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, entity));
            }
            else
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound,
                    "Material não localizado para alteração."));
        }
        #endregion


    }
}