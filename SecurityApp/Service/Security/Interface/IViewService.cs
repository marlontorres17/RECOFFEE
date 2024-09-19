using Entity.DTO.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Security.Interface
{
    public interface IViewService
    {
        Task<IEnumerable<ViewDto>> GetAll();
        Task<ViewDto> GetById(int id);
        Task Add(ViewDto viewDto);
        Task Update(ViewDto viewDto);
        Task DeleteLogically(int id);  // Eliminación lógica
        Task DeletePhysically(int id); // Eliminación física
    }
}
