using FluentValidation;
using GlamourJewels.Application.DTOs.CartItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Validations.CartItemValidations;

public class CartItemUpdateDtoValidator : AbstractValidator<CartItemUpdateDto>
{
    public CartItemUpdateDtoValidator()
    {
        RuleFor(x => x.Quantity).GreaterThan(0).When(x => x.Quantity.HasValue);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).When(x => x.Price.HasValue);
    }
}
