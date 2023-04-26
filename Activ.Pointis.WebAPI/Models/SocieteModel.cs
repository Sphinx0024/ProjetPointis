using Activ.Pointis.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activ.Pointis.WebAPI.Models
{
    public class SocieteModel
    {
        public static List<Societe> afficher()
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<Societe> donnees = (from p in _db.Societe
                                         select p).ToList();
                return donnees;
            }
        }

        public static List<Societe> AfficherUnSeul(long id)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<Societe> donnees = (from p in _db.Societe
                                         where p.ID == id
                                          select p).ToList();
                return donnees;
            }

        }


        public static void Ajouter(Societe societe)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                _db.Societe.Add(societe);
                _db.SaveChanges();
            }

        }

        public static void Modifier(long id, Societe societe)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<Societe> donnees = (from p in _db.Societe
                                         where p.ID == id
                                          select p).ToList();

                foreach (Societe societe1 in donnees)
                {
                    societe1.RaisonSociale = societe.RaisonSociale;
                    societe1.RCCM = societe.RCCM;
                    societe1.Telephone = societe.Telephone;
                    societe1.Email = societe.Email;

                }
                _db.SaveChanges();
            }
        }

        public static void supprimer(long id)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<Societe> donnees = (from p in _db.Societe
                                         where p.ID == id
                                          select p).ToList();

                _db.Societe.RemoveRange(donnees);
                _db.SaveChanges();
            }
        }
    }
}