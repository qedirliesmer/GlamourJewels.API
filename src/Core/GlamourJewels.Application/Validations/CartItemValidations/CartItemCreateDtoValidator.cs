using FluentValidation;
using GlamourJewels.Application.DTOs.CartItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Validations.CartItemValidations;

public class CartItemCreateDtoValidator:AbstractValidator<CartItemCreateDto>
{
    public CartItemCreateDtoValidator()
    {

        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
    }
}
