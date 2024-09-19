using Entity.Model.Context;
using Entity.Model.Operational;
using Microsoft.EntityFrameworkCore;
using Repository.Operational.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Operational.Implements
{
    public class LiquidationRepository : ILiquidationRepository
    {
        private readonly ApplicationDbContext _context;

        public LiquidationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Liquidation>> GetAll()
        {
            return await _context.liquidations.Where(l => l.State).ToListAsync(); // Solo trae los activos
        }

        public async Task<Liquidation> GetById(int id)
        {
            return await _context.liquidations.FirstOrDefaultAsync(l => l.Id == id && l.State); // Solo trae si está activo
        }

        public async Task Add(Liquidation liquidation)
        {
            await _context.liquidations.AddAsync(liquidation);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Liquidation liquidation)
        {
            _context.liquidations.Update(liquidation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogically(int id)
        {
            var liquidation = await _context.liquidations.FindAsync(id);
            if (liquidation != null)
            {
                liquidation.State = false; // Eliminación lógica
                _context.liquidations.Update(liquidation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePhysically(int id)
        {
            var liquidation = await _context.liquidations.FindAsync(id);
            if (liquidation != null)
            {
                _context.liquidations.Remove(liquidation); // Eliminación física
                await _context.SaveChangesAsync();
            }
        }
    }
}
