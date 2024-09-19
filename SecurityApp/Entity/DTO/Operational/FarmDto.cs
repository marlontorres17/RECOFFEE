using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO.Operational
{
    public class FarmDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double SizeMeter { get; set; }
        public byte[] Image { get; set; } // Usamos byte[] para almacenar la imagen
        public string Coordinate { get; set; }

        public int PersonaId { get; set; }

     

        public int CityId { get; set; }

        public Boolean State {  get; set; }
    }
}
