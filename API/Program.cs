using API.Middleware;
using Core.Entities;
using Core.Interfaces;
using infrastructure.Data;
using infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantContext>(opt => 
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IMenuItemRepository, MealRepository>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddCors();

builder.Services.AddSingleton<IConnectionMultiplexer>(config => 
{
    var connString = builder.Configuration.GetConnectionString("Redis") 
        ?? throw new Exception("Cannot get redis connection string");
    var Configuration = ConfigurationOptions.Parse(connString, true);
    return ConnectionMultiplexer.Connect(Configuration);
});

builder.Services.AddSingleton<ICartService, CartService>();

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>()
.AddEntityFrameworkStores<RestaurantContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));
    
app.MapControllers();

app.MapGroup("api").MapIdentityApi<AppUser>(); // api/login

try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<RestaurantContext>();
    await context.Database.MigrateAsync();
    await RestaurantContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    
    Console.WriteLine(ex);
    throw;
}

app.Run();
