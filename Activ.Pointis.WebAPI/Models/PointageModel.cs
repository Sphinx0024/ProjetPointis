using Activ.Pointis.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Activ.Pointis.WebAPI.Models
{
    public class PointageModel
    {
        public static List<V_Pointage> afficher()
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<V_Pointage> donnees = (from p in _db.V_Pointage
                                          select p).ToList();
                return donnees;
            }
        }

        public static long Point(long id)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                DateTime now = DateTime.Today;
                long ident = 0;

                List<V_Pointage> donnees = _db.V_Pointage.Where(d => d.PointageJour == now && d.EmployeID == id).OrderByDescending(d => d.PointageJour).ToList();

                if (donnees.Count > 0)
                {
                    foreach (var item in donnees)
                    {
                        ident = item.PointageID;
                    }  
                }
                return ident;
            }
        }

        public static List<V_Pointage> Jour(long id)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                DateTime now = DateTime.Today;

                List<V_Pointage> donnees = _db.V_Pointage.Where(d => d.PointageJour == now  && d.EmployeID == id).OrderByDescending(d => d.PointageJour).ToList();

                return donnees;
            }
        }

        public static List<V_Pointage> Semaine(long id)
        {
            using (PointisEntities _db = new PointisEntities())
            {

                DateTime dateDuJour = DateTime.Today;
                int delta = DayOfWeek.Monday - dateDuJour.DayOfWeek;
                DateTime debutDeLaSemaine = dateDuJour.AddDays(delta);
                DateTime finDeLaSemaine = debutDeLaSemaine.AddDays(6);

                List<V_Pointage> donnees = _db.V_Pointage.Where(d => d.PointageJour >= debutDeLaSemaine && d.PointageJour <= finDeLaSemaine && d.EmployeID==id).OrderByDescending(d => d.PointageJour).ToList();

                return donnees;
            }
        }

        public static List<V_Pointage> Mois(long id)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                DateTime now = DateTime.Today;

                DateTime startOfMonth = new DateTime(now.Year, now.Month, 1);

                DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                List<V_Pointage> donnees = _db.V_Pointage.Where(d => d.PointageJour >= startOfMonth && d.PointageJour <= endOfMonth && d.EmployeID == id).OrderByDescending(d => d.PointageJour).ToList();

                return donnees;
            }
        }


        public static List<V_Pointage> AfficherUnSeul(long id)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<V_Pointage> donnees = (from p in _db.V_Pointage
                                            where p.PointageID == id
                                          select p).ToList();
                return donnees;
            }

        }


        public static void Ajouter(Pointage pointage)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                _db.Pointage.Add(pointage);
                _db.SaveChanges();
            }

        }

        public static void Modifier(long id, Pointage pointage)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<Pointage> donnees = (from p in _db.Pointage
                                          where p.PointageID == id
                                          select p).ToList();

                foreach (Pointage point in donnees)
                {
                    point.Jour = point.Jour;
                    point.HeureEntree = point.HeureEntree;
                    point.HeureSortie = pointage.HeureSortie;
                    point.EmployesID = point.EmployesID;
                    point.Imei_employe_entree = pointage.Imei_employe_entree;
                    point.Imei_employe_sortie = point.Imei_employe_sortie;
                    point.latitude_entree = pointage.latitude_entree;
                    point.latitude_sortie = pointage.latitude_sortie;
                    point.longitude_entree = pointage.longitude_entree;
                    point.longitude_sortie = point.longitude_sortie;


                    _db.SaveChanges();
                }

                
            }
        }

        public static void supprimer(long id)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<Pointage> donnees = (from p in _db.Pointage
                                          where p.PointageID == id
                                          select p).ToList();

                _db.Pointage.RemoveRange(donnees);
                _db.SaveChanges();
            }
        }
    }
}