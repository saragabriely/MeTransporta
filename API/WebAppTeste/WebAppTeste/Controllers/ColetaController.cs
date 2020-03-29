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
    public class ColetaController : ApiController
    {
        private MTContext db = new MTContext();

        #region GET ALL - OK
        [Route("api/coleta")]
        public IEnumerable<Coleta> GetAll()
        {
            return db.Coletas.ToList();
        }
        #endregion

        #region GET - ID - OK
        [Route("api/coleta/{id}")]
        [HttpGet]
        public HttpResponseMessage GetColeta(int id)
        {
            db.Database.Connection.Open();

            var entity = db.Coletas.FirstOrDefault(e => e.IdColeta == id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Coleta com o Id = "
                        + id.ToString() + " não foi encontrada.");
            }
        }
        #endregion
        
        #region INSERT - OK

        [Route("api/coleta")]
        [HttpPost]
        [ResponseType(typeof(Material))]
        public IHttpActionResult Post([FromBody]Coleta coleta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Coletas.Add(coleta);
            db.SaveChanges();

            return CreatedAtRoute("Default", new { id = coleta.IdColeta }, coleta);
        }
        #endregion
        
        #region DELETE - OK
        
        [Route("api/coleta/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entity = db.Coletas.FirstOrDefault(e => e.IdColeta == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Coleta com o ID = "
                        + id.ToString() + " não encontrada para deletar.");
                }
                else
                {
                    db.Coletas.Remove(entity);
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
        [Route("api/coleta")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]Coleta coleta)
        {
            var entity = db.Coletas.FirstOrDefault(e => e.IdColeta == coleta.IdColeta);

            if (entity != null)
            {
                #region Atributos
                
                entity.EndRetCep              = coleta.EndRetCep;  
                entity.EndRetUf               = coleta.EndRetUf;  
                entity.EndRetEndereco         = coleta.EndRetEndereco;
                entity.EndRetNumero           = coleta.EndRetNumero; 
                entity.EndRetComplemento      = coleta.EndRetComplemento;    
                entity.EndRetBairro           = coleta.EndRetBairro;
                entity.EndRetCidade           = coleta.EndRetCidade;
                entity.EndRetNomeResponsavel  = coleta.EndRetNomeResponsavel;   
                entity.EndRetRespTelefone     = coleta.EndRetRespTelefone;  
                entity.EndEntCep              = coleta.EndEntCep;  
                entity.EndEntUf               = coleta.EndEntUf;  
                entity.EndEntEndereco         = coleta.EndEntEndereco;
                entity.EndEntNumero           = coleta.EndEntNumero; 
                entity.EndEntComplemento      = coleta.EndEntComplemento;    
                entity.EndEntBairro           = coleta.EndEntBairro;
                entity.EndEntCidade           = coleta.EndEntCidade;
                entity.EndEntNomeResponsavel  = coleta.EndEntNomeResponsavel;
                entity.EndEntRespTelefone     = coleta.EndEntRespTelefone;
                entity.DataMaxima             = coleta.DataMaxima;          
                entity.HorarioLimite          = coleta.HorarioLimite;          
                entity.ValorPretendido        = coleta.ValorPretendido;
                entity.Observacoes            = coleta.Observacoes;         
                entity.ApelidoColeta          = coleta.ApelidoColeta;
                entity.IdStatus               = coleta.IdStatus;

                entity.MatTipo                = coleta.MatTipo;
                entity.MatFragilidade         = coleta.MatFragilidade;
                entity.MatDescricao           = coleta.MatDescricao;
                entity.MatPeso                = coleta.MatPeso;
                entity.MatVolume              = coleta.MatVolume;
                entity.MatAltura              = coleta.MatAltura;
                entity.MatLargura             = coleta.MatLargura;
                entity.DataCadastro           = coleta.DataCadastro;
                entity.UltimaAtualizacao      = coleta.UltimaAtualizacao;

                entity.IdCliente              = coleta.IdCliente;

                entity.HorarioLimite02        = coleta.HorarioLimite02;
                entity.TipoVeiculo            = coleta.TipoVeiculo;
                entity.DescricaoStatus        = coleta.DescricaoStatus;

                #endregion

                db.SaveChanges();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, entity));
            }
            else
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound,
                    "Coleta não localizada para alteração."));
        }
        #endregion

    }
}