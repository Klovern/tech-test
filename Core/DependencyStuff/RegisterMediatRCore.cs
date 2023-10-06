using Core.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Core.DependencyStuff
{
    public static class RegisterMediatRCore
    {
        public static MediatRServiceConfiguration RegisterMediatRCoreStuff(this MediatRServiceConfiguration config)
        {
            config.RegisterServicesFromAssemblies(new[] { Assembly.GetExecutingAssembly(), Assembly.GetCallingAssembly() });
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenRequestPreProcessor(typeof(ValidationProcessor<>));
            return config;
        }

        public static IServiceCollection Xd(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
