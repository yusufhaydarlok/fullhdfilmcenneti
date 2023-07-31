using FluentValidation;
using fullhdfilmcenneti_core.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_service.Validations.UserDtoValidations
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                .Length(100, 2)
                .WithMessage("Kullanıcı Adınız 2 ila 100 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull()
                .Length(100, 2)
                .WithMessage("Adınız 2 ila 100 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull()
                .Length(100, 2)
                .WithMessage("Soyadınız 2 ila 100 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("E-posta adresi geçerli değil.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .Length(45, 6)
                .WithMessage("Şifreniz 6 ila 45 karakter uzunluğunda olmalıdır.");
        }
    }
}
