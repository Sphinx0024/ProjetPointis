using Activ.Pointis.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activ.Pointis.WebAPI.Models
{
    public class EquipeTravailModel
    {
        public static List<EquipeTravail> afficher()
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<EquipeTravail> donnees = (from p in _db.EquipeTravail
                                               select p).ToList();
                return donnees;
            }
        }

        public static List<EquipeTravail> AfficherUnSeul(long id)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<EquipeTravail> donnees = (from p in _db.EquipeTravail
                                               where p.ID == id
                                         select p).ToList();
                return donnees;
            }

        }


        public static void Ajouter(EquipeTravail equipeTravail)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                _db.EquipeTravail.Add(equipeTravail);
                _db.SaveChanges();
            }

        }

        public static void Modifier(long id, EquipeTravail equipeTravail)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<EquipeTravail> donnees = (from p in _db.EquipeTravail
                                         where p.ID == id
                                         select p).ToList();

                foreach (EquipeTravail equipeTravail1 in donnees)
                {
                    equipeTravail1.Title = equipeTravail.Title;
                    equipeTravail1.HeureDebutService = equipeTravail.HeureDebutService;
                    equipeTravail1.HeureFinService = equipeTravail.HeureFinService;
                    equipeTravail1.SocieteID = equipeTravail.SocieteID;

                }
                _db.SaveChanges();
            }
        }

        public static void supprimer(long id)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<EquipeTravail> donnees = (from p in _db.EquipeTravail
                                               where p.ID == id
                                         select p).ToList();

                _db.EquipeTravail.RemoveRange(donnees);
                _db.SaveChanges();
            }
        }
    }
}