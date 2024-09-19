using Entity.Model.Operational;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Operational.Interface
{
    public interface IHarvestRepository
    {
        Task<IEnumerable<Harvest>> GetAll();
        Task<Harvest> GetById(int id);
        Task Add(Harvest harvest);
        Task Update(Harvest harvest);
        Task DeleteLogically(int id); // Eliminación lógica
        Task DeletePhysically(int id); // Eliminación física
    }
}
