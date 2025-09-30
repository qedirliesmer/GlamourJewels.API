using FluentValidation;
using GlamourJewels.Application.DTOs.CartDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Validations.CartValidations;

public class CartUpdateDtoValidator:AbstractValidator<CartUpdateDto>
{
    public CartUpdateDtoValidator()
    {
        RuleFor(x => x.TotalAmount)
           .GreaterThanOrEqualTo(0)
           .When(x => x.TotalAmount.HasValue)
           .WithMessage("TotalAmount cannot be negative.");
    }
}
