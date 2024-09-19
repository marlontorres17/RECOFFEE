using Entity.Model.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Security.Interface
{
    public interface IViewRepository
    {
        Task<IEnumerable<View>> GetAll();
        Task<View> GetById(int id);
        Task Add(View view);
        Task Update(View view);
        Task DeleteLogically(int id);  // Eliminación lógica
        Task DeletePhysically(int id); // Eliminación física
    }
}
