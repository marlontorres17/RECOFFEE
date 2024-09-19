using Entity.Model.Context;
using Entity.Model.Operational;
using Microsoft.EntityFrameworkCore;
using Repository.Operational.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Operational.Implements
{
    public class HarvestRepository : IHarvestRepository
    {
        private readonly ApplicationDbContext _context;

        public HarvestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Harvest>> GetAll()
        {
            return await _context.harvests.Where(h => h.State).ToListAsync(); // Solo trae los activos
        }

        public async Task<Harvest> GetById(int id)
        {
            return await _context.harvests.FirstOrDefaultAsync(h => h.Id == id && h.State); // Solo trae si está activo
        }

        public async Task Add(Harvest harvest)
        {
            await _context.harvests.AddAsync(harvest);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Harvest harvest)
        {
            _context.harvests.Update(harvest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogically(int id)
        {
            var harvest = await _context.harvests.FindAsync(id);
            if (harvest != null)
            {
                harvest.State = false; // Eliminación lógica
                _context.harvests.Update(harvest);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePhysically(int id)
        {
            var harvest = await _context.harvests.FindAsync(id);
            if (harvest != null)
            {
                _context.harvests.Remove(harvest); // Eliminación física
                await _context.SaveChangesAsync();
            }
        }
    }
}
