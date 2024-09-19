using Entity.Model.Context;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Repository.Security.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Security.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.users.Where(u => u.State).ToListAsync(); // Traer solo Users activos
        }

        public async Task<User> GetById(int id)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.Id == id && u.State); // Traer solo si está activo
        }

        public async Task Add(User user)
        {
            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _context.users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogically(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user != null)
            {
                user.State = false; // Eliminación lógica
                _context.users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePhysically(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user != null)
            {
                _context.users.Remove(user); // Eliminación física
                await _context.SaveChangesAsync();
            }
        }
    }
}
