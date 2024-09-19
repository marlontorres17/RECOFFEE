using Entity.Model.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Parameter.Interface
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAll();
        Task<Country> GetById(int id);
        Task Add(Country country);
        Task Update(Country country);
        Task DeleteLogically(int id);  // Eliminación lógica
        Task DeletePhysically(int id); // Eliminación física
    }
}
