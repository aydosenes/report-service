using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IBaseMongoRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(Expression<Func<T, bool>> where);
        Task<T> GetByIdAsync(string id);
        Task<ICollection<T>> GetListAsync();
        Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> where);
        Task<T> AddAsync(T entity);
        Task<ICollection<T>> AddRangeAsync(ICollection<T> entities);
        Task<T> UpdateAsync(T entity);
        Task<ICollection<T>> UpdateRangeAsync(ICollection<T> entities);
        Task<T> DeleteAsync(T entity);
        Task<ICollection<T>> DeleteRangeAsync(ICollection<T> entities);
    }
}
