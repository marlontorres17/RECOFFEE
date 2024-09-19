using Entity.DTO.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Parameter.Interface
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDto>> GetAll();
        Task<CountryDto> GetById(int id);
        Task Add(CountryDto countryDto);
        Task Update(CountryDto countryDto);
        Task DeleteLogically(int id);  // Eliminación lógica
        Task DeletePhysically(int id); // Eliminación física
    }
}
