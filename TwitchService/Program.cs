using TwitchService.DataServices;
using TwitchService.EventHandlers;
using TwitchService.EventProcessing;
using System.Reflection;
using Core.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
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
