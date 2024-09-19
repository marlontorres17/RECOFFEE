using Entity.Model.Context;
using Entity.Model.Operational;
using Microsoft.EntityFrameworkCore;
using Repository.Operational.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Operational.Implements
{
    public class LotRepository : ILotRepository
    {
        private readonly ApplicationDbContext _context;

        public LotRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lot>> GetAll()
        {
            return await _context.lots.Where(l => l.State).ToListAsync(); // Solo trae los activos
        }

        public async Task<Lot> GetById(int id)
        {
            return await _context.lots.FirstOrDefaultAsync(l => l.Id == id && l.State); // Solo trae si está activo
        }

        public async Task Add(Lot lot)
        {
            await _context.lots.AddAsync(lot);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Lot lot)
        {
            _context.lots.Update(lot);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogically(int id)
        {
            var lot = await _context.lots.FindAsync(id);
            if (lot != null)
            {
                lot.State = false; // Eliminación lógica
                _context.lots.Update(lot);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePhysically(int id)
        {
            var lot = await _context.lots.FindAsync(id);
            if (lot != null)
            {
                _context.lots.Remove(lot); // Eliminación física
                await _context.SaveChangesAsync();
            }
        }
    }
}
