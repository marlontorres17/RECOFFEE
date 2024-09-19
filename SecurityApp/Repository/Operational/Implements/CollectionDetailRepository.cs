using Entity.Model.Context;
using Entity.Model.Operational;
using Microsoft.EntityFrameworkCore;
using Repository.Operational.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Operational.Implements
{
    public class CollectionDetailRepository : ICollectionDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public CollectionDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CollectionDetail>> GetAll()
        {
            return await _context.collectionDetails.Where(cd => cd.State).ToListAsync(); // Solo trae los activos
        }

        public async Task<CollectionDetail> GetById(int id)
        {
            return await _context.collectionDetails.FirstOrDefaultAsync(cd => cd.Id == id && cd.State); // Solo trae si está activo
        }

        public async Task Add(CollectionDetail collectionDetail)
        {
            await _context.collectionDetails.AddAsync(collectionDetail);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CollectionDetail collectionDetail)
        {
            _context.collectionDetails.Update(collectionDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogically(int id)
        {
            var collectionDetail = await _context.collectionDetails.FindAsync(id);
            if (collectionDetail != null)
            {
                collectionDetail.State = false; // Eliminación lógica
                _context.collectionDetails.Update(collectionDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePhysically(int id)
        {
            var collectionDetail = await _context.collectionDetails.FindAsync(id);
            if (collectionDetail != null)
            {
                _context.collectionDetails.Remove(collectionDetail); // Eliminación física
                await _context.SaveChangesAsync();
            }
        }
    }
}
