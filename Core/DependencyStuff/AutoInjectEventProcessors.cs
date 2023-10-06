using Core.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.DependencyStuff
{
    public static class AutoInjectEventProcessors
    {
        public static IServiceCollection InjectEventProcessors(this IServiceCollection services, Assembly currentExecutingAssembly)
        {
            Console.WriteLine("--> Attempting to register EventProcessors<T>");

            foreach (Type eventProcessor in currentExecutingAssembly.GetTypes().Where(IsSubclassOfProcessorGeneric))
            {
                var eventDefinition = eventProcessor.BaseType;
   
                if (eventDefinition != null)
                {
                    Type genericArgument = eventDefinition.GetGenericArguments()[0];
                    services.AddSingleton(typeof(IEventProcessor<>).MakeGenericType(genericArgument), eventProcessor);
                    Console.WriteLine($"--> registered EventProcessor '{eventProcessor.FullName}'");
                }
            }
            return services;
        }

        // https://stackoverflow.com/questions/457676/check-if-a-class-is-derived-from-a-generic-class
        private static bool IsSubclassOfProcessorGeneric(Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (typeof(EventProcessor<>) == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }
    }
}
