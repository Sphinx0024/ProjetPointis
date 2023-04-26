using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetScan.Model
{
    public class PointageModel
    {
        public long PointageID { get; set; }
        public System.DateTime Jour { get; set; }
        public System.DateTime HeureEntree { get; set; }
        public Nullable<System.DateTime> HeureSortie { get; set; }
        public Nullable<long> EmployesID { get; set; }
        public string Imei_employe_entree { get; set; }
        public string Imei_employe_sortie { get; set; }
        public string Imei_verificateur_entree { get; set; }
        public string Imei_verificateur_sortie { get; set; }
        public string Application_id_entree { get; set; }
        public string Application_id_sortie { get; set; }
    }
}
