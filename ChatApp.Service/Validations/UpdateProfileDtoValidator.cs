using ChatApp.Shared.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Service.Validations
{
    public class UpdateProfileDtoValidator: AbstractValidator<UpdateProfileDto>
    {
        public UpdateProfileDtoValidator() 
        {
            RuleFor(x => x.UserName)
                    .NotNull().WithMessage("Bu alan gereklidir").NotEmpty().WithMessage("Bu alan gereklidir");
            RuleFor(x => x.Email)
                .NotNull().WithMessage("Bu alan gereklidir").NotEmpty().WithMessage("Bu alan gereklidir");
        }
    }
}
