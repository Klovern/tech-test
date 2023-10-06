using TwitchService.DataServices;
using TwitchService.EventHandlers;
using TwitchService.EventProcessing;
using System.Reflection;
using Core.Events;
using Core.DependencyStuff;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.InjectEventProcessors(Assembly.GetExecutingAssembly());
builder.Services.AddHostedService<TwitchServiceClient>();
builder.Services.AddScoped<IEventConsumer<UserCreatedEvent>, EventConsumerA>();
builder.Services.AddScoped<IEventConsumer<UserCreatedEvent>, EventConsumerB>();
builder.Services.AddScoped<MediatRConsumerA>();
builder.Services.AddScoped<MediatRConsumerB>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
