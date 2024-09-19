using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO.Operational
{
    public class HarvestDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double KiloPrice { get; set; }
        public double DailyPrice { get; set; }

        public int LotId { get; set; }

        public Boolean state { get; set; }
    }
}
