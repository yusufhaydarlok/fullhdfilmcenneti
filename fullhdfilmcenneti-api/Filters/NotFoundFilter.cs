using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using fullhdfilmcenneti_core.Models;
using fullhdfilmcenneti_core.DTOs;
using fullhdfilmcenneti_core.Services;
using fullhdfilmcenneti_core.Repositories;

namespace fullhdfilmcenneti_api.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IGenericRepository<T> _repository;

        public NotFoundFilter(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();

            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (Guid)idValue;
            var anyEntity = await _repository.AnyAsync(x => x.Id == id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id}) not found"));
        }
    }
}
