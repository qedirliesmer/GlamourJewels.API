using FluentValidation;
using GlamourJewels.Application.DTOs.ProductTagDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Validations.ProductTagValidations;

public class ProductTagCreateDtoValidator : AbstractValidator<ProductTagCreateDto>
{
    public ProductTagCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tag adı boş ola bilməz")
            .MaximumLength(100).WithMessage("Tag adı maksimum 100 simvol ola bilər");

        RuleFor(x => x.Slug)
            .MaximumLength(150).WithMessage("Slug maksimum 150 simvol ola bilər");
    }
}