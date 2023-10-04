using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UserService.Data;
using UserService.DataServices;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("UsersConnection")));
}

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddSingleton<IUserServiceClient, UserServiceClient>();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddHealthChecks();

var app = builder.Build();

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.PrepPopulation(builder.Environment.IsProduction());
app.Run();
