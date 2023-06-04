using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);

        Task<bool> ExistsAsync(int id);
    }
}
