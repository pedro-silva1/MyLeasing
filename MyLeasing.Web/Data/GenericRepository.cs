using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
        }

        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<T>().AnyAsync(x => x.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await SaveAllAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await SaveAllAsync();
        }
    }
}
