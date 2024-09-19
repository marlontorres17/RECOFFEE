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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.departments.Where(d => d.State).ToListAsync(); // Solo trae los activos
        }

        public async Task<Department> GetById(int id)
        {
            return await _context.departments.FirstOrDefaultAsync(d => d.Id == id && d.State); // Solo trae si está activo
        }

        public async Task Add(Department department)
        {
            await _context.departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Department department)
        {
            _context.departments.Update(department);
            await _context.SaveChangesAsync();
        }

        // Método para eliminación lógica
        public async Task DeleteLogically(int id)
        {
            var department = await _context.departments.FindAsync(id);
            if (department != null)
            {
                department.State = false;  // Eliminación lógica
                _context.departments.Update(department);  // Actualiza el registro, se actualizará el campo DeletedAt automáticamente
                await _context.SaveChangesAsync();
            }
        }

        // Método para eliminación física
        public async Task DeletePhysically(int id)
        {
            var department = await _context.departments.FindAsync(id);
            if (department != null)
            {
                _context.departments.Remove(department);  // Elimina físicamente el registro
                await _context.SaveChangesAsync();
            }
        }
    }
}
