using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Activ.Pointis.Data;
using Zen.Barcode;

namespace Activ.Pointis.WebAPI.Models
{
    public class EmployesModel
    {
        public static List<Employes> afficher()
        {
            using(PointisEntities _db = new PointisEntities())
            {
                List<Employes> donnees = (from p in _db.Employes
                                              select p).OrderByDescending(p=>p.EmployeID).ToList();
                return donnees;
            }
        }

        public static List<Employes> AfficherUnSeul(long id)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<Employes> donnees = (from p in _db.Employes
                                          where p.EmployeID == id
                                          select p).ToList();
                return donnees;
            }
        }


        /*public static List<Employes> AfficherUn(string mat)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<Employes> donnees = (from p in _db.Employes
                                          where p.Matricule == mat
                                          select p).ToList();
                return donnees;
            }

        }*/


        public static long Ajouter(Employes employes)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                _db.Employes.Add(employes);
                _db.SaveChanges();
            }

            return employes.EmployeID;

        }

        public static void Modifier(long id,Employes employes) {
            using (PointisEntities _db = new PointisEntities())
            {
                List<Employes> donnees = (from p in _db.Employes
                                          where p.EmployeID == id
                                          select p).ToList();

                foreach(Employes emp in donnees)
                {
                    emp.Nom = employes.Nom;
                    emp.Prenom = employes.Prenom;
                    emp.Email = employes.Email;
                    emp.Telephone = employes.Telephone;
                    emp.Sexe = employes.Sexe;
                    emp.Matricule = employes.Matricule;
                    emp.Titre = employes.Titre;
                    emp.SocieteID= employes.SocieteID;
                    emp.EquipeID= employes.EquipeID;
                    emp.Password = employes.Password;
                }
                _db.SaveChanges();
            }
        }

        public static void supprimer(long id)
        {
            using(PointisEntities _db = new PointisEntities())
            {
                List<Employes> donnees = (from p in _db.Employes
                                          where p.EmployeID == id
                                          select p).ToList();

                _db.Employes.RemoveRange(donnees);
                _db.SaveChanges();
            }
        }

        public static System.Drawing.Image GenererQrcode(Employes employes)
        {
            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            var infos = (employes.Nom + " " + employes.Prenom + " " + employes.Telephone + " " + employes.Email + " " + employes.Sexe);
            var result = qrcode.Draw(infos,60);

            return result;
        }

        public static System.Drawing.Image GenererQrcodeParId(long id)
        {
            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            var infos = (id.ToString());
            var result = qrcode.Draw(infos, 60);

            return result;
        }

    }
}