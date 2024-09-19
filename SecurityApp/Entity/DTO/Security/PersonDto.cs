using Entity.Model.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO.Security
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirtsName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int CityId { get; set; }
        public string TypeDocument { get; set; }
        public string NumberDocument { get; set; }
        public Boolean State { get; set; }
    }
}
