using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO.Security
{
    public class RoleViewDto
    { 
        public int Id { get; set; }

        public int RoleId { get; set; }
        public  int ViewId {  get; set; } 

        public Boolean State {  get; set; }
    }
}
