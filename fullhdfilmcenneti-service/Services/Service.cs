using AutoMapper;
using fullhdfilmcenneti_core.DTOs;
using fullhdfilmcenneti_core.Models;
using fullhdfilmcenneti_core.Repositories;
using fullhdfilmcenneti_core.Services;
using fullhdfilmcenneti_core.UnitOfWorks;
using fullhdfilmcenneti_core.Utilities;
using fullhdfilmcenneti_service.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_service.Services
{
    public class Service<T,R> : IService<T,R> where T : BaseEntity where R : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto<R>> AddAsync(T entity)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
            var entry = await _repository.FindAsync(x => !x.IsDeleted && x.Id == entity.Id);
            if (entry != null)
            {
                return CustomResponseDto<R>.Fail(404, $"{typeof(T).ToString().Split(".").Last()} is already exist!");
            }
            var response = await _repository.AddAsync(entity);
            if (response == 0) return CustomResponseDto<R>.Fail(404,ErrorMessages.SOMETHING_WENT_WRONG);

            var entityDTO = _mapper.Map<R>(entity);
            return CustomResponseDto<R>.Success(entityDTO);
        }

        public async Task<CustomResponseDto<IEnumerable<R>>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<CustomResponseDto<IEnumerable<R>>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.FindAsync(expression);
        }

        public async Task<CustomResponseDto<IEnumerable<R>>> GetAllAsync()
        {
            var response = await _repository.GetAll().ToListAsync();
            var result = _mapper.Map<IEnumerable<R>>(response);
            return result;
        }

        public async Task<CustomResponseDto<R>> GetByIdAsync(Guid id)
        {
            var hasUser = await _repository.GetByIdAsync(id);
            if (hasUser == null)
            {
                throw new NotFoundExcepiton($"{typeof(T).Name}({id}) not found");
            }
            var result = _mapper.Map<R>(hasUser);
            return result;
        }

        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<CustomResponseDto<R>> Where(Expression<Func<T, bool>> expression)
        {
            var where = _repository.Where(expression);
            var result = _mapper.Map<R>(where);
            return result;
        }
    }
}
