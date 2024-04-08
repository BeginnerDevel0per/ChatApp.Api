using ChatApp.Shared.DTOs.UserDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Service.Validations
{
    public class UpdatePasswordDtoValidator : AbstractValidator<UpdatePasswordDto>
    {
        public UpdatePasswordDtoValidator()
        {

              
            RuleFor(x => x.CurrentPassword).NotNull().WithMessage("Bu alan gereklidir").NotEmpty().WithMessage("Bu alan gereklidir");
            RuleFor(x => x.Password).NotNull().WithMessage("Bu alan gereklidir").NotEmpty().WithMessage("Bu alan gereklidir");
            RuleFor(x => x.PasswordAgain).NotNull().WithMessage("Bu alan gereklidir").NotEmpty().WithMessage("Bu alan gereklidir");
            RuleFor(x => x.Password).Equal(x => x.PasswordAgain).WithMessage("Şifreler Uyuşmuyor");
        }
    }
}
