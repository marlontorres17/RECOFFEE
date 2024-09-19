using Entity.Model.Context;
using Entity.Model.Operational;
using Microsoft.EntityFrameworkCore;
using Repository.Operational.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Operational.Implements
{
    public class FarmRepository : IFarmRepository
    {
        private readonly ApplicationDbContext _context;

        public FarmRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todas las fincas activas
        public async Task<IEnumerable<Farm>> GetAll()
        {
            return await _context.farms.Where(f => f.State).ToListAsync(); // Solo trae las fincas activas
        }

        // Obtener finca por Id (si está activa)
        public async Task<Farm> GetById(int id)
        {
            return await _context.farms.FirstOrDefaultAsync(f => f.Id == id && f.State); // Solo trae si está activa
        }

        // Agregar nueva finca
        public async Task Add(Farm farm)
        {
            if (farm.Image != null && farm.Image.Length > 0)
            {
                // Asegúrate de que la imagen no sea nula o vacía
                _context.farms.Add(farm);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("La imagen no puede ser nula o vacía.");
            }
        }

        // Actualizar finca
        public async Task Update(Farm farm)
        {
            if (farm != null)
            {
                _context.farms.Update(farm);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("La finca no puede ser nula.");
            }
        }

        // Eliminación lógica
        public async Task DeleteLogically(int id)
        {
            var farm = await _context.farms.FindAsync(id);
            if (farm != null)
            {
                farm.State = false; // Marcamos como inactiva
                _context.farms.Update(farm);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("La finca con el id proporcionado no fue encontrada.");
            }
        }

        // Eliminación física
        public async Task DeletePhysically(int id)
        {
            var farm = await _context.farms.FindAsync(id);
            if (farm != null)
            {
                _context.farms.Remove(farm); // Eliminación física
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("La finca con el id proporcionado no fue encontrada.");
            }
        }
    }
}
