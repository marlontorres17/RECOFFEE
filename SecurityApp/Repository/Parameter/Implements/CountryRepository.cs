using Entity.Model.Context;
using Entity.Model.Parameter;
using Microsoft.EntityFrameworkCore;
using Repository.Parameter.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Parameter.Implements
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            return await _context.countrys.Where(c => c.State).ToListAsync(); // Solo trae los activos
        }

        public async Task<Country> GetById(int id)
        {
            return await _context.countrys.FirstOrDefaultAsync(c => c.Id == id && c.State); // Solo trae si está activo
        }

        public async Task Add(Country country)
        {
            await _context.countrys.AddAsync(country);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Country country)
        {
            _context.countrys.Update(country);
            await _context.SaveChangesAsync();
        }

        // Método para eliminación lógica
        public async Task DeleteLogically(int id)
        {
            var country = await _context.countrys.FindAsync(id);
            if (country != null)
            {
                country.State = false;  // Eliminación lógica
                _context.countrys.Update(country);  // Actualiza el registro, se actualizará el campo DeletedAt automáticamente
                await _context.SaveChangesAsync();
            }
        }

        // Método para eliminación física
        public async Task DeletePhysically(int id)
        {
            var country = await _context.countrys.FindAsync(id);
            if (country != null)
            {
                _context.countrys.Remove(country);  // Elimina físicamente el registro
                await _context.SaveChangesAsync();
            }
        }
    }
}
