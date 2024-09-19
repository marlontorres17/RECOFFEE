using Entity.Model.Context;
using Entity.Model.Operational;
using Microsoft.EntityFrameworkCore;
using Repository.Operational.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Operational.Implements
{
    public class PersonBenefitRepository : IPersonBenefitRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonBenefitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PersonBenefit>> GetAll()
        {
            return await _context.personBenefits.Where(pb => pb.State).ToListAsync(); // Solo trae los activos
        }

        public async Task<PersonBenefit> GetById(int id)
        {
            return await _context.personBenefits.FirstOrDefaultAsync(pb => pb.Id == id && pb.State); // Solo trae si está activo
        }

        public async Task Add(PersonBenefit personBenefit)
        {
            await _context.personBenefits.AddAsync(personBenefit);
            await _context.SaveChangesAsync();
        }

        public async Task Update(PersonBenefit personBenefit)
        {
            _context.personBenefits.Update(personBenefit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogically(int id)
        {
            var personBenefit = await _context.personBenefits.FindAsync(id);
            if (personBenefit != null)
            {
                personBenefit.State = false; // Eliminación lógica
                _context.personBenefits.Update(personBenefit);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePhysically(int id)
        {
            var personBenefit = await _context.personBenefits.FindAsync(id);
            if (personBenefit != null)
            {
                _context.personBenefits.Remove(personBenefit); // Eliminación física
                await _context.SaveChangesAsync();
            }
        }
    }
}
