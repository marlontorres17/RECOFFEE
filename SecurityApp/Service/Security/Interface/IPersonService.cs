using Entity.DTO.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Security.Interface
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDto>> GetAll();
        Task<PersonDto> GetById(int id);
        Task Add(PersonDto personDto);
        Task Update(PersonDto personDto);
        Task DeleteLogically(int id);  // Eliminación lógica
        Task DeletePhysically(int id); // Eliminación física
    }
}
