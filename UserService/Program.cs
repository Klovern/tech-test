using Microsoft.EntityFrameworkCore;
using UserService.Data;

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
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHealthChecks();

var app = builder.Build();

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.PrepPopulation(builder.Environment.IsProduction());
app.Run();
//using Microsoft.EntityFrameworkCore;
//using UserService.Data;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();

//if (builder.Environment.IsDevelopment())
//{
//    builder.Services.AddEndpointsApiExplorer();
//    builder.Services.AddSwaggerGen();

//    builder.Services.AddDbContext<AppDbContext>(opt =>
//                opt.UseInMemoryDatabase("InMem"));
//}
//else
//{
//    builder.Services.AddDbContext<AppDbContext>(opt =>
//            opt.UseSqlite(builder.Configuration.GetConnectionString("UsersConnection")));
//}
//builder.Services.AddScoped<IUserRepo, UserRepo>();

//var app = builder.Build();

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();
//app.PrepPopulation(builder.Environment.IsProduction());
//app.Run();
