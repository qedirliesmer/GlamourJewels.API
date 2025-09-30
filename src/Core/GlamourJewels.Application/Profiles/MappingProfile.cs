using AutoMapper;
using GlamourJewels.Application.DTOs.CartDTOs;
using GlamourJewels.Application.DTOs.CartItemDTOs;
using GlamourJewels.Application.DTOs.CategoryDTOs;
using GlamourJewels.Application.DTOs.FavoriteDTOs;
using GlamourJewels.Application.DTOs.OrderDTOs;
using GlamourJewels.Application.DTOs.OrderItemDTOs;
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

        CreateMap<Order, OrderResponseDto>();

        CreateMap<OrderCreateDto, Order>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending"))
            .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.PaymentMethod != "CashOnDelivery"))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<OrderUpdateDto, Order>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<OrderItemCreateDto, OrderItem>();
        CreateMap<OrderItemUpdateDto, OrderItem>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<OrderItem, OrderItemResponseDto>();

        CreateMap<Cart, CartResponseDto>();
        CreateMap<CartCreateDto, Cart>();
        CreateMap<CartUpdateDto, Cart>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CartItemCreateDto, CartItem>();
        CreateMap<CartItemUpdateDto, CartItem>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<CartItem, CartItemResponseDto>();

        CreateMap<Favorite, FavoriteResponseDto>();
        CreateMap<FavoriteCreateDto, Favorite>();
    }

}
