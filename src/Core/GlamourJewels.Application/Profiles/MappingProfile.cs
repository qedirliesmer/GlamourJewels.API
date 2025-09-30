using AutoMapper;
using GlamourJewels.Application.DTOs.CategoryDTOs;
using GlamourJewels.Application.DTOs.ProductDTOs;
using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Profiles;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductResponseDto>()
           .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
           .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl)));

        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();

        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();

        CreateMap<Category, CategoryResponseDto>();
    }

}
