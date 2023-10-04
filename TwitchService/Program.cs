using Microsoft.Extensions.DependencyInjection;
using Polly;
using TwitchService.DataServices;
using TwitchService.EventHandlers;
using TwitchService.EventProcessing;
using TwitchService.SyncDataServices;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddHostedService<TwitchServiceClient>();
builder.Services.AddScoped<UserCreatedRequestHandler>();
builder.Services.AddScoped<AnotherCreatedRequestHandler>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
