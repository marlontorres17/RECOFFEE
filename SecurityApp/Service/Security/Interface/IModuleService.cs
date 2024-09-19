using Entity.DTO.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Security.Interface
{
    public interface IModuleService
    {
        Task<IEnumerable<ModuleDto>> GetAll();
        Task<ModuleDto> GetById(int id);
        Task Add(ModuleDto moduleDto);
        Task Update(ModuleDto moduleDto);
        Task DeleteLogically(int id);  // Eliminación lógica
        Task DeletePhysically(int id); // Eliminación física
    }
}
