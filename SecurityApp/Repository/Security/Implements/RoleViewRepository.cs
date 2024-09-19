using Entity.Model.Context;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Repository.Security.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Security.Implements
{
    public class RoleViewRepository : IRoleViewRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleViewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleView>> GetAll()
        {
            return await _context.roles_views.Where(rv => rv.State).ToListAsync(); // Traer solo RoleViews activos
        }

        public async Task<RoleView> GetById(int id)
        {
            return await _context.roles_views.FirstOrDefaultAsync(rv => rv.Id == id && rv.State); // Traer solo si está activo
        }

        public async Task Add(RoleView roleView)
        {
            await _context.roles_views.AddAsync(roleView);
            await _context.SaveChangesAsync();
        }

        public async Task Update(RoleView roleView)
        {
            _context.roles_views.Update(roleView);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogically(int id)
        {
            var roleView = await _context.roles_views.FindAsync(id);
            if (roleView != null)
            {
                roleView.State = false; // Eliminación lógica
                _context.roles_views.Update(roleView);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePhysically(int id)
        {
            var roleView = await _context.roles_views.FindAsync(id);
            if (roleView != null)
            {
                _context.roles_views.Remove(roleView); // Eliminación física
                await _context.SaveChangesAsync();
            }
        }
    }
}
