﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activ.Pointis.Android.PersonnelUI.Models
{
    public class Employes
    {
        public long EmployeID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Sexe { get; set; }
        public long SocieteID { get; set; }
        public string Titre { get; set; }
        public string Matricule { get; set; }
    }
}
