using FluentValidation;
using GlamourJewels.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Validations.UserValidations;

public class UserLoginDtoValidator:AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz.")
            .EmailAddress().WithMessage("Email düzgün formatda deyil.");

        // Password: boş olmamalıdır, minimum 6 simvol, 
        // böyük hərf, kiçik hərf, rəqəm və xüsusi simvol tələbi
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə boş ola bilməz.")
            .MinimumLength(8).WithMessage("Şifrə minimum 8 simvol olmalıdır.");
    }
}
