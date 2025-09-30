using FluentValidation;
using GlamourJewels.Application.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Validations.OrderValidations;
public class OrderCreateDtoValidator:AbstractValidator<OrderCreateDto>
{
    public OrderCreateDtoValidator()
    {
        RuleFor(x => x.ShippingAddress)
            .NotEmpty().WithMessage("Shipping address is required.")
            .MaximumLength(500);

        RuleFor(x => x.BillingAddress)
            .NotEmpty().WithMessage("Billing address is required.")
            .MaximumLength(500);

        RuleFor(x => x.PaymentMethod)
            .NotEmpty().WithMessage("Payment method is required.");
    }

}
