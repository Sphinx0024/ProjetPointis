using Activ.Pointis.WebAPI.Models;
using Activ.Pointis.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Activ.Pointis.WebAPI.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    //[Authorize]
    public class PointageController : ApiController
    {
        // GET: api/Pointage
        [HttpGet]
        public IEnumerable<V_Pointage> Get()
        {
            return PointageModel.afficher();
        }

        // GET: api/Pointage/5
        [HttpGet]
        public IEnumerable<V_Pointage> Get(long id)
        {
            return PointageModel.AfficherUnSeul(id);
        }

        [ActionName("Point")]
        public IHttpActionResult Point(long id)
        {
            long ident = PointageModel.Point(id);
            return Ok(ident);
        }

        [ActionName("Jour")]
        public IEnumerable<V_Pointage> Jour(long id)
        {
            return PointageModel.Jour(id);
        }

        [ActionName("Semaine")]
        public IEnumerable<V_Pointage> Semaine(long id)
        {
            return PointageModel.Semaine(id);
        }

        [ActionName("Mois")]
        public IEnumerable<V_Pointage> Mois(long id)
        {
            return PointageModel.Mois(id);
        }

        // POST: api/Pointage
        [HttpPost]
        public void Post([FromBody]Pointage pointage)
        {
            PointageModel.Ajouter(pointage);
        }

        // PUT: api/Pointage/5
        [HttpPut]
        public void Put(long id, [FromBody] Pointage pointage)
        {
            PointageModel.Modifier(id, pointage);
        }

        // DELETE: api/Pointage/5
        [HttpDelete]
        public void Delete(long id)
        {
            PointageModel.supprimer(id);
        }
    }
}
