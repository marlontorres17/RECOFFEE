using Entity.Model.Context;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Repository.Security.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Security.Implements
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.roles.Where(r => r.State).ToListAsync(); // Traer solo roles activos
        }

        public async Task<Role> GetById(int id)
        {
            return await _context.roles.FirstOrDefaultAsync(r => r.Id == id && r.State); // Traer solo si está activo
        }

        public async Task Add(Role role)
        {
            await _context.roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Role role)
        {
            _context.roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogically(int id)
        {
            var role = await _context.roles.FindAsync(id);
            if (role != null)
            {
                role.State = false; // Eliminación lógica
                _context.roles.Update(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePhysically(int id)
        {
            var role = await _context.roles.FindAsync(id);
            if (role != null)
            {
                _context.roles.Remove(role); // Eliminación física
                await _context.SaveChangesAsync();
            }
        }
    }
}
