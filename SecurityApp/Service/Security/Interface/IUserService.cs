using Entity.DTO.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Security.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task Add(UserDto userDto);
        Task Update(UserDto userDto);
        Task DeleteLogically(int id);  // Eliminación lógica
        Task DeletePhysically(int id); // Eliminación física
    }
}
