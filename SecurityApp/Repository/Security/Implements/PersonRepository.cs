using Entity.Model.Context;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Repository.Security.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Security.Implements
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _context.persons.Where(p => p.State).ToListAsync(); // Traer solo personas activas
        }

        public async Task<Person> GetById(int id)
        {
            return await _context.persons.FirstOrDefaultAsync(p => p.Id == id && p.State); // Traer solo si está activa
        }

        public async Task Add(Person person)
        {
            await _context.persons.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Person person)
        {
            _context.persons.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogically(int id)
        {
            var person = await _context.persons.FindAsync(id);
            if (person != null)
            {
                person.State = false; // Eliminación lógica
                _context.persons.Update(person);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePhysically(int id)
        {
            var person = await _context.persons.FindAsync(id);
            if (person != null)
            {
                _context.persons.Remove(person); // Eliminación física
                await _context.SaveChangesAsync();
            }
        }
    }
}
