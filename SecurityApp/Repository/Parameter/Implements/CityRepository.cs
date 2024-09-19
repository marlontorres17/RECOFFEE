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
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            return await _context.citys.Where(c => c.State).ToListAsync(); // Solo trae los activos
        }

        public async Task<City> GetById(int id)
        {
            return await _context.citys.FirstOrDefaultAsync(c => c.Id == id && c.State); // Solo trae si está activo
        }

        public async Task Add(City city)
        {
            await _context.citys.AddAsync(city);
            await _context.SaveChangesAsync();
        }

        public async Task Update(City city)
        {
            _context.citys.Update(city);
            await _context.SaveChangesAsync();
        }

        // Método para eliminación lógica
        public async Task DeleteLogically(int id)
        {
            var city = await _context.citys.FindAsync(id);
            if (city != null)
            {
                city.State = false;  // Eliminación lógica
                _context.citys.Update(city);  // Actualiza el registro, se actualizará el campo DeletedAt automáticamente
                await _context.SaveChangesAsync();
            }
        }

        // Método para eliminación física
        public async Task DeletePhysically(int id)
        {
            var city = await _context.citys.FindAsync(id);
            if (city != null)
            {
                _context.citys.Remove(city);  // Elimina físicamente el registro
                await _context.SaveChangesAsync();
            }
        }
    }
}
