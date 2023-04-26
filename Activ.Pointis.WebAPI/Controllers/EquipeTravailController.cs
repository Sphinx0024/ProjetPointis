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
    public class EquipeTravailController : ApiController
    {
        [HttpGet]
        // GET: api/Societe
        public IEnumerable<EquipeTravail> Get()
        {
            return EquipeTravailModel.afficher();
        }

        [HttpGet]
        // GET: api/Societe/5
        public List<EquipeTravail> Get(int id)
        {
            return EquipeTravailModel.AfficherUnSeul(id);
        }

        [HttpPost]
        // POST: api/Societe
        public void Post([FromBody] EquipeTravail equipeTravail)
        {
            EquipeTravailModel.Ajouter(equipeTravail);
        }

        [HttpPut]
        // PUT: api/Societe/5
        public void Put(long id, [FromBody] EquipeTravail equipeTravail)
        {
            EquipeTravailModel.Modifier(id, equipeTravail);
        }

        [HttpDelete]
        // DELETE: api/Societe/5
        public void Delete(long id)
        {
            EquipeTravailModel.supprimer(id);
        }
    }
}
