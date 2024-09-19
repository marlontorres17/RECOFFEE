using Entity.Model.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Security.Interface
{
    public interface IModuleRepository
    {
        Task<IEnumerable<Module>> GetAll();
        Task<Module> GetById(int id);
        Task Add(Module module);
        Task Update(Module module);
        Task DeleteLogically(int id); // Eliminación lógica
        Task DeletePhysically(int id); // Eliminación física
    }
}
