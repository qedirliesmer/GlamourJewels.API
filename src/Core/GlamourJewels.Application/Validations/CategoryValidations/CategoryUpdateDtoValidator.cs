using FluentValidation;
using GlamourJewels.Application.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Validations.CategoryValidations;

public class CategoryUpdateDtoValidator:AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name daxil edilməlidir.")
            .MaximumLength(100).WithMessage("Name maksimum 100 simvol ola bilər.");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug daxil edilməlidir.")
            .MaximumLength(100).WithMessage("Slug maksimum 100 simvol ola bilər.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description maksimum 500 simvol ola bilər.");

        RuleFor(x => x.ImageUrl)
            .Must(uri => string.IsNullOrEmpty(uri) || Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("ImageUrl düzgün URL formatında olmalıdır.");

        RuleFor(x => x.MetaTitle)
            .MaximumLength(150).WithMessage("MetaTitle maksimum 150 simvol ola bilər.");

        RuleFor(x => x.MetaDescription)
            .MaximumLength(300).WithMessage("MetaDescription maksimum 300 simvol ola bilər.");
    }
}
