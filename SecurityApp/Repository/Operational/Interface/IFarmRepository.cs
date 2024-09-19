using Entity.Model.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Operational.Interface
{
    public interface IFarmRepository
    {
        Task<IEnumerable<Farm>> GetAll();              
        Task<Farm> GetById(int id);
        Task Add(Farm farm);                           
        Task Update(Farm farm);                        
        Task DeleteLogically(int id);                  
        Task DeletePhysically(int id);                 
    }
}
