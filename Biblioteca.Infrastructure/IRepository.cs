using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infrastructure
{
    public interface IRepository<T>
    {
        Task<T> InsertAsync(T entity);
        
        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(int id);

        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();


        Task<IEnumerable<T>> SearchASync(Expression<Func<T, bool>> predicate);



    }
}
