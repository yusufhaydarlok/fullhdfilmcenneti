using FluentValidation;
using fullhdfilmcenneti_core.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_service.Validations.UserDtoValidations
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("ID boş olamaz.");
        }
    }
}
