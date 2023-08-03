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
        Task<CustomResponseDto<R>> CreateAsync(T entity);
        Task<CustomResponseDto<R>> UpdateAsync(T entity);
        Task<CustomResponseDto<bool>> RemoveAsync(T entity);
    }
}
