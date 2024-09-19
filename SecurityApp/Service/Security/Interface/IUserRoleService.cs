using Entity.DTO.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Security.Interface
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleDto>> GetAll();
        Task<UserRoleDto> GetById(int id);
        Task Add(UserRoleDto userRoleDto);
        Task Update(UserRoleDto userRoleDto);
        Task DeleteLogically(int id);  // Eliminación lógica
        Task DeletePhysically(int id); // Eliminación física
    }
}
