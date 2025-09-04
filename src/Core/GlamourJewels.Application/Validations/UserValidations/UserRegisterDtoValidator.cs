using FluentValidation;
using GlamourJewels.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Validations.UserValidations;

public class UserRegisterDtoValidator:AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(x => x.Fullname)
            .NotEmpty().WithMessage("Fullname boş ola bilməz.")
            .MinimumLength(3).WithMessage("Fullname minimum 3 simvol olmalıdır.")
            .MaximumLength(50).WithMessage("Fullname maksimum 50 simvol ola bilər.");

        // Email: boş olmamalı və email formatında olmalıdır
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz.")
            .EmailAddress().WithMessage("Email düzgün formatda deyil.");

        // Password: boş olmamalıdır, minimum 6 simvol, 
        // böyük hərf, kiçik hərf, rəqəm və xüsusi simvol tələbi
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə boş ola bilməz.")
            .MinimumLength(6).WithMessage("Şifrə minimum 6 simvol olmalıdır.")
            .Matches("[A-Z]").WithMessage("Şifrədə ən azı bir böyük hərf olmalıdır.")
            .Matches("[a-z]").WithMessage("Şifrədə ən azı bir kiçik hərf olmalıdır.")
            .Matches("[0-9]").WithMessage("Şifrədə ən azı bir rəqəm olmalıdır.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Şifrədə ən azı bir xüsusi simvol olmalıdır.");
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName boş ola bilməz.")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("UserName yalnız hərf və rəqəmdən ibarət ola bilər.");

    }
}
