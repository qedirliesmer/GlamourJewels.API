using FluentValidation;
using GlamourJewels.Application.DTOs.ProductSpecificationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Validations.ProductSpecificationValidations;

public class ProductSpecificationCreateDtoValidator : AbstractValidator<ProductSpecificationCreateDto>
{
    public ProductSpecificationCreateDtoValidator()
    {
        RuleFor(x => x.Key).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Value).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Unit).MaximumLength(50);
        RuleFor(x => x.SpecificationCategory).MaximumLength(100);
        RuleFor(x => x.ProductId).NotEmpty();
    }
}
