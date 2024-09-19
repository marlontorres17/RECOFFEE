using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO.Operational
{
    public class LotDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SizeMeters { get; set; }
        public byte[] Image { get; set; }

        public int FarmId { get; set; }

        public Boolean State {  get; set; } 
    }
}
