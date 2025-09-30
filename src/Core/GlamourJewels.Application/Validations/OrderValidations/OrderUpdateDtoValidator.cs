using FluentValidation;
using GlamourJewels.Application.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Validations.OrderValidations;

public class OrderUpdateDtoValidator:AbstractValidator<OrderUpdateDto>
{
    public OrderUpdateDtoValidator()
    {
        RuleFor(x => x.Status)
            .MaximumLength(50);

        RuleFor(x => x.ShippingAddress)
            .MaximumLength(500);

        RuleFor(x => x.BillingAddress)
            .MaximumLength(500);

        RuleFor(x => x.PaymentMethod)
            .MaximumLength(50);
    }
}
