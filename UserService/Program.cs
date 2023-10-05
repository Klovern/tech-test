using Core.Validation;
using FluentValidation;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserService.Data;
using UserService.DataServices;
using UserService.Validators;

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

builder.Services.AddControllers(opt =>
{
    //opt.Filters.Add
});
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddSingleton<IUserServiceClient, UserServiceClient>();


/*
 * Add MediatR
 */
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenRequestPreProcessor(typeof(ValidationProcessor<>));
});


/*
 * Fluent Validators
 */
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();

/*
 * AutoMapper
 */
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());



var app = builder.Build();

//app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ValidationMappingMiddleware>();
app.MapControllers();
app.PrepPopulation(builder.Environment.IsProduction());
app.Run();
