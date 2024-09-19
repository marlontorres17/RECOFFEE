using Entity.DTO.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Security.Interface
{
    public interface IRoleViewService
    {
        Task<IEnumerable<RoleViewDto>> GetAll();
        Task<RoleViewDto> GetById(int id);
        Task Add(RoleViewDto roleViewDto);
        Task Update(RoleViewDto roleViewDto);
        Task DeleteLogically(int id);  // Eliminación lógica
        Task DeletePhysically(int id); // Eliminación física
    }
}
