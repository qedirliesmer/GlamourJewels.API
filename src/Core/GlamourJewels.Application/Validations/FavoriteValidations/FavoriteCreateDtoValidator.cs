using FluentValidation;
using GlamourJewels.Application.DTOs.FavoriteDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Validations.FavoriteValidations;

public class FavoriteCreateDtoValidator : AbstractValidator<FavoriteCreateDto>
{
    public FavoriteCreateDtoValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
    }
}