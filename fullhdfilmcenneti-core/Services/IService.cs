using fullhdfilmcenneti_core.DTOs;
using fullhdfilmcenneti_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_core.Services
{
    public interface IService<T, R> where T : BaseEntity where R : class
    {
        Task<CustomResponseDto<R>> GetByIdAsync(Guid id);
        Task<CustomResponseDto<IEnumerable<R>>> GetAllAsync();
        IQueryable<CustomResponseDto<R>> Where(Expression<Func<T, bool>> expression);
        Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<T, bool>> expression);
        Task<CustomResponseDto<R>> AddAsync(T entity);
        Task<CustomResponseDto<IEnumerable<R>>> AddRangeAsync(IEnumerable<T> entities);
        Task<CustomResponseDto<IEnumerable<R>>> FindAsync(Expression<Func<T, bool>> expression);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
