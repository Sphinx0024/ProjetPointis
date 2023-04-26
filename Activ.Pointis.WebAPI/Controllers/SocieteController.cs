using Activ.Pointis.Data;
using Activ.Pointis.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Activ.Pointis.WebAPI.Controllers
{
    public class SocieteController : ApiController
    {
        [HttpGet]
        // GET: api/Societe
        public IEnumerable<Societe> Get()
        {
            return SocieteModel.afficher() ;
        }

        [HttpGet]
        // GET: api/Societe/5
        public List<Societe> Get(int id)
        {
            return SocieteModel.AfficherUnSeul(id);
        }

        [HttpPost]
        // POST: api/Societe
        public void Post([FromBody] Societe societe)
        {
            SocieteModel.Ajouter(societe);
        }

        [HttpPut]
        // PUT: api/Societe/5
        public void Put(long id, [FromBody] Societe societe)
        {
            SocieteModel.Modifier(id, societe);
        }

        [HttpDelete]
        // DELETE: api/Societe/5
        public void Delete(long id)
        {
            SocieteModel.supprimer(id);
        }
    }
}
