using AuctionService.Data;
using AuctionService.RequestHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuctionDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg => {}, typeof(MappingProfiles).Assembly);
var app = builder.Build();


app.UseAuthorization();

app.MapControllers();

try
{
    DbInitializer.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine($"Error seeding database: {e.Message}");
}

app.Run();
