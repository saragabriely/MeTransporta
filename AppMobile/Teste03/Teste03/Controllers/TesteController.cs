namespace Teste03.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]

    //[RoutePrefix("api/v1/teste")]
    public class TesteController 
    {
       /* private const string Value = "ERRO!";
        private ITesteService _service;

        public TesteController()
        {
        }

        public TesteController(ITesteService service)
        {
            _service = service;
        }

        public IEnumerable<teste> GetAll()
        {
            using (MeTransportaEntities entities = new MeTransportaEntities())
            {
                return entities.teste.ToList();
                //return _service.GetAll();
            }
        } /*webapimt2.scm.azurewebsites.net:443*/

        /*
        [Route("")]
        [HttpGet]
        public Task<HttpResponseMessage> Get(int idTeste)
        {
        } * /
        //[Route("")]
        [HttpGet]
        public void GetTeste(int id)
        {
            using (MeTransportaEntities4 entities = new MeTransportaEntities4())
            {
                entities.Database.Connection.Open();
                //entities.Connection.Open();
                //entities.Database.Connection.Open();

                var entity = entities.teste.FirstOrDefault(e => e.idTeste == id);
                if (entity != null)
                {
                    Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, "Teste com o Id = " + id.ToString() + " not found.");
                }
            }
        }

        /* POST - INSERT * /
        //[Route("")]
        [HttpPost]
        public HttpResponseMessage Insert([FromBody]teste Teste)
        {
            try
            {
                using (MeTransportaEntities4 entities = new MeTransportaEntities4())
                {
                    entities.teste.Add(Teste);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, Teste);

                    message.Headers.Location = new Uri(Request.RequestUri + Teste.idTeste.ToString());

                    return Request.CreateResponse(HttpStatusCode.Accepted, message);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /* DELETE * /
        //[Route("")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (MeTransportaEntities4 entities = new MeTransportaEntities4())
                {
                    var entity = entities.teste.FirstOrDefault(e => e.idTeste == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Teste com o ID = " + id.ToString() + " não encontrado para deletar.");
                    }
                    else
                    {
                        entities.teste.Remove(entity);
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

        //[Route("")]
        [HttpPut]
        public HttpResponseMessage Update(teste Teste, [FromBody] int id)
        {
            try
            {
                using (MeTransportaEntities4 entities = new MeTransportaEntities4())
                {
                    var entity = entities.teste.FirstOrDefault(e => e.idTeste == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Teste com o Id = " + id.ToString() + " não encontrado para atualizar.");
                    }
                    else
                    {
                        entity.idTeste = Teste.idTeste;
                        entity.descricao = Teste.descricao;

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
    }
}
