using Entity.Model.Context;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Repository.Security.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Security.Implements
{
    public class ViewRepository : IViewRepository
    {
        private readonly ApplicationDbContext _context;

        public ViewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<View>> GetAll()
        {
            return await _context.views.Where(v => v.State).ToListAsync(); // Traer solo Views activos
        }

        public async Task<View> GetById(int id)
        {
            return await _context.views.FirstOrDefaultAsync(v => v.Id == id && v.State); // Solo si está activo
        }

        public async Task Add(View view)
        {
            await _context.views.AddAsync(view);
            await _context.SaveChangesAsync();
        }

        public async Task Update(View view)
        {
            _context.views.Update(view);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogically(int id)
        {
            var view = await _context.views.FindAsync(id);
            if (view != null)
            {
                view.State = false; // Eliminación lógica
                _context.views.Update(view);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePhysically(int id)
        {
            var view = await _context.views.FindAsync(id);
            if (view != null)
            {
                _context.views.Remove(view); // Eliminación física
                await _context.SaveChangesAsync();
            }
        }
    }
}
