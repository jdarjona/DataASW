using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using wsTRH;

namespace wsTRH.Controllers
{
    [System.Web.Http.Authorize]
    public class ListadoMonitorizacionController : ApiController
    {
        private navision db = new navision();

        // GET: api/ListadoMonitorizacion
        [ResponseType(typeof(vListadoPedidosMonitorizacion))]
        public IHttpActionResult Get()
        {
            var listado = db.vListadoPedidosMonitorizacion.OrderBy(c => c.indice).ToList();
            
            listado.All(l => {
                if (l.Fecha_Carga_Requerida.HasValue) { 
                    l.Fecha_Carga_Requerida = l.Fecha_Carga_Requerida.Value.ToUniversalTime();
                }
                return true;
            });

            return Ok(listado.ToArray());
        }

        // GET: api/ListadoMonitorizacion/5
        [ResponseType(typeof(vListadoPedidosMonitorizacion))]
        public IHttpActionResult Get(string id)
        {
            vListadoPedidosMonitorizacion vListadoPedidosMonitorizacion = db.vListadoPedidosMonitorizacion.Find(id);
            if (vListadoPedidosMonitorizacion == null)
            {
                return NotFound();
            }

            return Ok(vListadoPedidosMonitorizacion);
        }

        // PUT: api/ListadoMonitorizacion/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Put()
        {
            try
            {
                IFirebaseConfig config = new FirebaseConfig
                                            {
                                                AuthSecret = "XQsDE173GieFhbMUUs2t2OD5eUwZFjjrEsAYbq6B",
                                                BasePath = "https://flickering-fire-4088.firebaseio.com/"
                                            };
                 IFirebaseClient _client = new FirebaseClient(config);
                var todo = new 
                {
                    name = "Execute PUSH",
                    priority = 2
                };
                PushResponse response =  await _client.PushAsync("todos/push", todo);
                string a= response.Result.Name;//The result will contain the child name of the new data that was added
                return StatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        // POST: api/ListadoMonitorizacion
        [ResponseType(typeof(vListadoPedidosMonitorizacion))]
        public IHttpActionResult Post(vListadoPedidosMonitorizacion vListadoPedidosMonitorizacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.vListadoPedidosMonitorizacion.Add(vListadoPedidosMonitorizacion);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (vListadoPedidosMonitorizacionExists(vListadoPedidosMonitorizacion.Cod__Agrupacion_Pedido))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vListadoPedidosMonitorizacion.Cod__Agrupacion_Pedido }, vListadoPedidosMonitorizacion);
        }

        // DELETE: api/ListadoMonitorizacion/5
        [ResponseType(typeof(vListadoPedidosMonitorizacion))]
        public IHttpActionResult Delete(string id)
        {
            vListadoPedidosMonitorizacion vListadoPedidosMonitorizacion = db.vListadoPedidosMonitorizacion.Find(id);
            if (vListadoPedidosMonitorizacion == null)
            {
                return NotFound();
            }

            db.vListadoPedidosMonitorizacion.Remove(vListadoPedidosMonitorizacion);
            db.SaveChanges();

            return Ok(vListadoPedidosMonitorizacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool vListadoPedidosMonitorizacionExists(string id)
        {
            return db.vListadoPedidosMonitorizacion.Count(e => e.Cod__Agrupacion_Pedido == id) > 0;
        }
    }
}