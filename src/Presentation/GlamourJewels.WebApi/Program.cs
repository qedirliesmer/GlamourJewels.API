using GlamourJewels.Application.Abstracts.Repositories;
using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.Profiles;
using GlamourJewels.Application.Shared;
using GlamourJewels.Application.Shared.Helpers;
using GlamourJewels.Application.Shared.Settings;
using GlamourJewels.Domain.Entities;
using GlamourJewels.Infrastructure.Services;
using GlamourJewels.Persistence.Contexts;
using GlamourJewels.Persistence.Repositories;
using GlamourJewels.Persistence.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using AutoMapper;
using GlamourJewels.Application.Profiles;
using static GlamourJewels.Application.Shared.Permissions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "My API",
            Version = "v1"
        });

        // ?? JWT auth üçün Security Definition
        c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "JWT Token-i buraya yaz?n. Format: Bearer {token}"
        });

        // ?? Security Requirement
        c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    });
builder.Services.AddDbContext<GlamourJewelsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
}
);
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
    //options.Lockout.AllowedForNewUsers = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
})
    .AddEntityFrameworkStores<GlamourJewelsDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JWTSettings>();
//var emailSettings=builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();

builder.Services.AddAuthorization(options =>
{
    foreach (var permission in PermissionHelper.GetAllPermissionList().Concat(CartPermissions.All).Concat(FavoritePermissions.All).Concat(ReviewPermissions.All).Concat(ProductImagePermissions.All).Concat(ProductSpecificationPermissions.All))
    {
        options.AddPolicy(permission, policy =>
        {
            policy.RequireClaim("Permission", permission);
        });
    }
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // dev üçün false, prod-da true qoy
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
});
builder.Services.AddScoped<IFileService>(provider =>
{
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    return new GlamourJewels.Infrastructure.Services.FileService(env.WebRootPath);
});
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IProductImageRepository, IProductImageRepository>();
builder.Services.AddScoped<IProductSpecificationService, ProductSpecificationService>();
builder.Services.AddScoped<IProductSpecificationRepository, ProductSpecificationRepository>();
builder.Services.AddScoped<IProductTagRepository, ProductTagRepository>();
builder.Services.AddScoped<IProductTagService, ProductTagService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();








builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
