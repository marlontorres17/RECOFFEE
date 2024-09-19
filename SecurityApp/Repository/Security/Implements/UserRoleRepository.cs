using Entity.Model.Context;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Repository.Security.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Security.Implements
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserRole>> GetAll()
        {
            return await _context.users_roles.Where(ur => ur.State).ToListAsync(); // Traer solo UserRoles activos
        }

        public async Task<UserRole> GetById(int id)
        {
            return await _context.users_roles.FirstOrDefaultAsync(ur => ur.Id == id && ur.State); // Solo si está activo
        }

        public async Task Add(UserRole userRole)
        {
            await _context.users_roles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task Update(UserRole userRole)
        {
            _context.users_roles.Update(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogically(int id)
        {
            var userRole = await _context.users_roles.FindAsync(id);
            if (userRole != null)
            {
                userRole.State = false; // Eliminación lógica
                _context.users_roles.Update(userRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePhysically(int id)
        {
            var userRole = await _context.users_roles.FindAsync(id);
            if (userRole != null)
            {
                _context.users_roles.Remove(userRole); // Eliminación física
                await _context.SaveChangesAsync();
            }
        }
    }
}
