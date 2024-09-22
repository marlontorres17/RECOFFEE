using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO.Operational
{
    public class LiquidationDto : BaseDto
    {
      
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TotalKilo { get; set; }
        public string TotalDay { get; set; }
        public string TotalBenefit { get; set; }
        public string TotalPay { get; set; }

        public int PersonId { get; set; }
    
    }
}
