using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Domain.Entities;
using GlamourJewels.Persistence.Contexts;
using GlamourJewels.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
builder.Services.AddScoped<IFileService>(provider =>
{
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    return new GlamourJewels.Persistence.Services.FileService(env.WebRootPath);
});
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
