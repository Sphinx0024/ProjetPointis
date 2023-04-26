using Activ.Pointis.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activ.Pointis.WebAPI.Models
{
    public class ConnexionModel
    {
        public static List<Connexion> afficher()
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<Connexion> donnees = (from p in _db.Connexion
                                           select p).ToList();
                return donnees;
            }
        }

        public static List<Connexion> AfficherUnSeul(long id)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<Connexion> donnees = (from p in _db.Connexion
                                           where p.ConnexionID == id
                                         select p).ToList();
                return donnees;
            }

        }



        public static string Connecter(ConnexionClasse connexionClasse)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                string id = null;
                List<Connexion> donnees = (from p in _db.Connexion
                                           where p.Login ==connexionClasse.Login && p.Password == connexionClasse.Password
                                           select p).ToList();

                if (donnees.Count > 0)
                {
                    foreach (Connexion connexion in donnees)
                    {
                        var idConnex = connexion.ConnexionID;
                        var idSoc = connexion.SocieteID;
                        id = idConnex + "#" + idSoc;
                        return id;
                    }
                }
                return id;
            }

        }

        public static string Connect(string login, string passe)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                string id = null;
                List<Connexion> donnees = (from p in _db.Connexion
                                           where p.Login == login && p.Password == passe
                                           select p).ToList();

                if(donnees.Count > 0)
                {
                    foreach(Connexion connexion in donnees)
                    {
                        var idConnex = connexion.ConnexionID;
                        var idEmp = connexion.SocieteID;
                        id = idConnex + "#" + idEmp;
                        return id;
                    }
                }
                return id;
            }

        }

        /*public static long EmployesID(string tel)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                long id = 0;
                List<Connexion> donnees = (from p in _db.Connexion
                                           where p.Login == tel
                                           select p).ToList();

                if (donnees.Count > 0)
                {
                    foreach (Connexion connexion in donnees)
                    {
                        id = (long)connexion.EmployeID;
                        return id;
                    }
                }
                return id;
            }

        }*/

        public static string Verifier(string tel)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                string code = "";
                List<Connexion> donnees = (from p in _db.Connexion
                                           where p.Login == tel
                                           select p).ToList();
                if (donnees.Count > 0)
                {
                    code = GenerateCode();
                }
                return code;
            }

        }

        public static long Confirmer(string tel, string passe)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                long code = 0;
                List<Connexion> donnees = (from p in _db.Connexion
                                           where p.Login == tel && p.Password == passe
                                           select p).ToList();
                if (donnees.Count > 0)
                {
                    foreach(Connexion p in donnees)
                    {
                        code = (long)p.SocieteID;
                    }
                }
                return code;
            }

        }

        /*public static Connexion Verifier(string tel)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                Connexion connexion = new Connexion();
                List<Connexion> donnees = (from p in _db.Connexion
                                           where p.Login == tel
                                           select p).ToList();
                if (donnees.Count > 0)
                {
                    foreach (Connexion connexion1 in donnees)
                    {
                            connexion.Login = connexion1.Login;
                            connexion.Password = GenerateCode();
                            connexion.EmployeID = connexion1.EmployeID;
                            connexion.Role = connexion1.Role;

                        return connexion;
                    }
                   
                }
                return connexion;
            }

        }*/


        public static void Ajouter(Connexion connexion)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                _db.Connexion.Add(connexion);
                _db.SaveChanges();
            }

        }

        public static void Modifier(long id, Connexion connexion)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<Connexion> donnees = (from p in _db.Connexion
                                           where p.ConnexionID == id
                                         select p).ToList();

                foreach (Connexion connexion1 in donnees)
                {
                    connexion1.Login = connexion.Login;
                    connexion1.Password = connexion.Password;
                    connexion1.SocieteID = connexion.SocieteID;
                    connexion1.Role = connexion.Role;
                    _db.SaveChanges();
                }
                
            }
        }

        /*public static void ModifierPasse(long id, Connexion connexion)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<Connexion> donnees = (from p in _db.Connexion
                                           where p.ConnexionID == id
                                           select p).ToList();

                foreach (Connexion connexion1 in donnees)
                {
                    connexion1.Login = connexion1.Login;
                    connexion1.Password = connexion.Password;
                    connexion1.EmployeID = connexion1.EmployeID;
                    connexion1.Role = connexion1.Role;
                    _db.SaveChanges();
                }

            }
        }*/

        public static void supprimer(long id)
        {
            using (PointisEntities _db = new PointisEntities())
            {
                List<Connexion> donnees = (from p in _db.Connexion
                                           where p.ConnexionID == id
                                         select p).ToList();

                _db.Connexion.RemoveRange(donnees);
                _db.SaveChanges();
            }
        }

        public static string GenerateCode()
        {
            Random rand = new Random();
            int code = rand.Next(1000, 9999);
            return code.ToString();
        }

        public static bool IsValidUser(string login, string pwd)
        {
            
            using (PointisEntities _db = new PointisEntities())
            {
                bool isValid = false;
                var donnees = (from c in _db.Connexion
                                          where c.Login == login && c.Password == pwd
                                          select c);
                 if(donnees.Any())
                {
                    return isValid;
                }
                 else 
                {
                    isValid = true;
                    return isValid;
                }

            }
        }
    }
}