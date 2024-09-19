using Entity.Model.Context;
using Entity.Model.Operational;
using Microsoft.EntityFrameworkCore;
using Repository.Operational.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Operational.Implements
{
    public class BenefitRepository : IBenefitRepository
    {
        private readonly ApplicationDbContext _context;

        public BenefitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Benefit>> GetAll()
        {
            return await _context.benefits.Where(b => b.State).ToListAsync(); // Solo trae los activos
        }

        public async Task<Benefit> GetById(int id)
        {
            return await _context.benefits.FirstOrDefaultAsync(b => b.Id == id && b.State); // Solo trae si está activo
        }

        public async Task Add(Benefit benefit)
        {
            await _context.benefits.AddAsync(benefit);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Benefit benefit)
        {
            _context.benefits.Update(benefit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogically(int id)
        {
            var benefit = await _context.benefits.FindAsync(id);
            if (benefit != null)
            {
                benefit.State = false; // Eliminación lógica
                _context.benefits.Update(benefit);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePhysically(int id)
        {
            var benefit = await _context.benefits.FindAsync(id);
            if (benefit != null)
            {
                _context.benefits.Remove(benefit); // Eliminación física
                await _context.SaveChangesAsync();
            }
        }
    }
}
