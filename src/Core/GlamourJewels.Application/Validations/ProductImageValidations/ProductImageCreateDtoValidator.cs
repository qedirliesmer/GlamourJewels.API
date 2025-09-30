using FluentValidation;
using GlamourJewels.Application.DTOs.ProductImageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Validations.ProductImageValidations;

public class ProductImageCreateDtoValidator : AbstractValidator<ProductImageCreateDto>
{
    public ProductImageCreateDtoValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.ImageUrl).NotEmpty().MaximumLength(500);
        RuleFor(x => x.ThumbnailUrl).MaximumLength(500);
        RuleFor(x => x.AltText).MaximumLength(200);
        RuleFor(x => x.Title).MaximumLength(200);
        RuleFor(x => x.DisplayOrder).GreaterThanOrEqualTo(0);
    }
}