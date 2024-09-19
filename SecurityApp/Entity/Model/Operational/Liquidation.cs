using Entity.Model.Security;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model.Operational
{
    public class Liquidation : BaseEntity
    {
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TotalKilo {  get; set; }
        public string TotalDay { get; set; }
        public string TotalBenefit { get; set; }
        public string TotalPay {  get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }


    }
}
