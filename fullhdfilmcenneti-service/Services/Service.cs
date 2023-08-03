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
        public async Task<CustomResponseDto<R>> GetByIdAsync(Guid id)
        {
            var hasUser = await _repository.GetByIdAsync(id);
            if (hasUser == null)
            {
                throw new NotFoundExcepiton($"{typeof(T).Name}({id}) not found");
            }
            var result = _mapper.Map<R>(hasUser);
            return CustomResponseDto<R>.Success(200, result);
        }

        public async Task<CustomResponseDto<IEnumerable<R>>> GetAllAsync()
        {
            var response = await _repository.GetAll().ToListAsync();
            var result = _mapper.Map<IEnumerable<R>>(response);
            return CustomResponseDto<IEnumerable<R>>.Success(200, result);
        }

        public async Task<CustomResponseDto<R>> CreateAsync(T entity)
        {
            var entry = await _repository.FindAsync(x => !x.IsDeleted && x.Id == entity.Id);
            if (entry != null)
            {
                return CustomResponseDto<R>.Fail(400, $"{typeof(T).ToString().Split(".").Last()} is already exist!");
            }
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            var entityDTO = _mapper.Map<R>(entity);
            return CustomResponseDto<R>.Success(200 ,entityDTO);
        }

        public async Task<CustomResponseDto<R>> UpdateAsync(T entity)
        {
            var entry = await _repository.FindAsync(x => !x.IsDeleted && x.Id == entity.Id);
            if (entry.Count() <= 0)
            {
                return CustomResponseDto<R>.Fail(400, $"{typeof(T).ToString().Split(".").Last()} is not found!");
            }
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            var updated = _mapper.Map<R>(entity);
            return CustomResponseDto<R>.Success(200 ,updated);
        }

        public async Task<CustomResponseDto<bool>> RemoveAsync(T entity)
        {
            var entry = await _repository.FindAsync(x => !x.IsDeleted && x.Id == entity.Id);
            if (entry.Count() <= 0)
            {
                return CustomResponseDto<bool>.Fail(400, $"{typeof(T).ToString().Split(".").Last()} is not found!");
            }
            _repository.SoftRemoveAsync(entry.First());
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<bool>.Success(200, true);
        }
    }
}
