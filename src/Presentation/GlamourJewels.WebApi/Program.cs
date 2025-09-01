using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<GlamourJewelsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
}
);
builder.Services.AddScoped<IFileService>(provider =>
{
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    return new GlamourJewels.Persistence.Services.FileService(env.WebRootPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
