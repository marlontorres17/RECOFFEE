using Entity.Model.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Security.Interface
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetAll();
        Task<UserRole> GetById(int id);
        Task Add(UserRole userRole);
        Task Update(UserRole userRole);
        Task DeleteLogically(int id);  // Eliminación lógica
        Task DeletePhysically(int id); // Eliminación física
    }
}
