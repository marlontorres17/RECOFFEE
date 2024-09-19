using Entity.Model.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Security.Interface
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAll();
        Task<Person> GetById(int id);
        Task Add(Person person);
        Task Update(Person person);
        Task DeleteLogically(int id);  // Eliminación lógica
        Task DeletePhysically(int id); // Eliminación física
    }
}
