using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppTeste.Models;
using HttpPostAttribute   = System.Web.Mvc.HttpPostAttribute;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute    = System.Web.Http.HttpGetAttribute;
using HttpPutAttribute    = System.Web.Http.HttpPutAttribute;
using WebAppTeste.Interface;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Text;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace WebAppTeste.Controllers
{

// newtonsoft - conversao do objeto em json (e vice-versa)
// web client - comunicação com o web service

    //[RoutePrefix("api/teste")]
    public class TesteController : ApiController
    {
        private MTContext db = new MTContext();

        public HttpClient client = new HttpClient();

        //private ITesteRepository _service;// = new ITesteRepository();

        #region GETALL - OK
        public IEnumerable<teste> GetAll()
        {
            if(db != null)
            {
                return db.Testes.ToList();
            }
            return null;
        }

        #endregion

        #region GET - ID - OK
        //[Route("")]
        [HttpGet]
        public HttpResponseMessage GetTeste(int id)
        {
            var entity = db.Testes.FirstOrDefault(e => e.IdTeste == id);
            if (entity != null)
            {
               return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
               return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Teste com o Id = " 
                        + id.ToString() + " not found.");
            }
        }
        #endregion

        #region GET - STRING 
        //[Route("")]
        [HttpGet]
        public HttpResponseMessage GetTeste(string desc)
        {
            var entity = db.Testes.FirstOrDefault(e => e.descricao == desc);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Teste com a descrição = "
                         + desc.ToString() + " não encontrado.");
            }
        }
        #endregion


        #region INSERT - OK
        //POST - INSERT 
        //[Route("")]
        [HttpPost]
        [ResponseType(typeof(teste))]
        public IHttpActionResult Post([FromBody]teste Teste)
        {                    
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                    
            db.Testes.Add(Teste);
            db.SaveChanges();
                
            return CreatedAtRoute("Default", new { id = Teste.IdTeste }, Teste);
        }
        #endregion

        #region DELETE - OK
        // DELETE 
        //[Route("")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entity = db.Testes.FirstOrDefault(e => e.IdTeste == id);
                    
                if (entity == null)
                {
                   return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Teste com o ID = " + id.ToString() + " não encontrado para deletar.");
                }
                else
                {
                    db.Testes.Remove(entity);
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

        #region UPDATE - OK
        [HttpPut] 
        public IHttpActionResult Put([FromBody]teste objeto)
        {
            var entity = db.Testes.FirstOrDefault(l => l.IdTeste == objeto.IdTeste);

            if (entity != null)
            {
                entity.IdTeste   = objeto.IdTeste;
                entity.descricao = objeto.descricao;
                db.SaveChanges();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, entity));
            }
            else
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, 
                    "Teste não localizado para alteração."));
        }
        #endregion

        #region UPDATE - COMENTADOS ***

        /*
        //[Route("")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody]teste objeto)
        {
            //teste Teste = objeto.lista.FirstOrDefault(l => l.IdTeste == objeto.IdTeste);

            //Collection<teste> lista = new Collection<teste>();

            teste Teste = lista.FirstOrDefault(l => l.IdTeste == objeto.IdTeste);

            var entity = entities.Testes.FirstOrDefault(e => e.IdTeste == id);

            if (Teste != null)
            {
                Teste.IdTeste   = objeto.IdTeste;
                Teste.descricao = objeto.descricao;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            }
            else
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, 
                    "Teste não localizado para alteração."));
        } */

        /*
        public HttpResponseMessage Update([FromBody] int id)
        {
            try
            {
                using (MTContext entities = new MTContext())
                {
                    teste Teste = new teste();

                    var entity = entities.Testes.FirstOrDefault(e => e.IdTeste == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Teste com o Id = " + id.ToString() + " não encontrado para atualizar.");
                    }
                    else
                    {
                        entity.IdTeste   = Teste.IdTeste;
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
        }
        */


        /*
         //[Route("")]
        [HttpPut]
        public HttpResponseMessage Update([FromBody] int id)
        {
            try
            {
                using (MTContext entities = new MTContext())
                {
                    teste Teste = new teste();

                    var entity = entities.Testes.FirstOrDefault(e => e.IdTeste == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Teste com o Id = " + id.ToString() + " não encontrado para atualizar.");
                    }
                    else
                    {
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
        }
         */

        /*
         
        public HttpResponseMessage Get(int id)
        {
            using (MTContext entities = new MTContext())
            {
                var entity = entities.Bancos.FirstOrDefault(e => e.IdBanco == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Banco com o Id = "
                        + ToString() + " não encontrado.");
                }
            }
        }

         */

        /*
        // GET: Todos
        public ActionResult Index()
        {
            Trace.WriteLine("GET /Testes/Index");



            return db.Testes.ToList();

            //return View(db.Testes.ToList());
        }

        // GET: Todos/Details/5
        public ActionResult Details(int? id)
        {
            Trace.WriteLine("GET /Testes/Details/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teste Teste = db.Testes.Find(id);
            if (Teste == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(Teste);
        }

        // GET: Todos/Create
        public ActionResult Create()
        {
            Trace.WriteLine("GET /Testes/Create");
            return View(new teste { CreatedDate = DateTime.Now });
        }

        // POST: Todos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Description,CreatedDate")] teste Teste)
        {
            Trace.WriteLine("POST /Todos/Create");
            if (ModelState.IsValid)
            {
                db.Testes.Add(Teste);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Teste);
        }

        // GET: Todos/Edit/5
        public ActionResult Edit(int? id)
        {
            Trace.WriteLine("GET /Testes/Edit/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teste Teste = db.Testes.Find(id);
            if (Teste == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //return HttpNotFound();
            }
            return View(Teste);
        }

        // POST: Todos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Description,CreatedDate")] teste Teste)
        {
            Trace.WriteLine("POST /Testes/Edit/" + Teste.idTeste);
            if (ModelState.IsValid)
            {
                db.Entry(Teste).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Teste);
        }

        // GET: Todos/Delete/5
        public ActionResult Delete(int? id)
        {
            Trace.WriteLine("GET /Testes/Delete/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teste Teste = db.Testes.Find(id);
            if (Teste == null)
            {
                return HttpNotFound();
            }
            return View(Teste);
        }

        // POST: Todos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trace.WriteLine("POST /Testes/Delete/" + id);
            teste Teste = db.Testes.Find(id);
            db.Testes.Remove(Teste);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        */
        #endregion
    }
}
