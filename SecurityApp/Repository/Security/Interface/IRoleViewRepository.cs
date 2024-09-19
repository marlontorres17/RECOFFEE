using Entity.Model.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Security.Interface
{
    public interface IRoleViewRepository
    {
        Task<IEnumerable<RoleView>> GetAll();
        Task<RoleView> GetById(int id);
        Task Add(RoleView roleView);
        Task Update(RoleView roleView);
        Task DeleteLogically(int id);  // Eliminación lógica
        Task DeletePhysically(int id); // Eliminación física
    }
}
