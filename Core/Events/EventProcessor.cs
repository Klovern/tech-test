using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Core.Events
{
    public abstract class EventProcessor<T> : IEventProcessor<T> where T : class, new()
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public EventProcessor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task ProcessEvent(string eventMessage, CancellationToken cancellationToken)
        {
            try
            {
                var eventType = JsonSerializer.Deserialize<T>(eventMessage);
                Console.WriteLine("---> Message passed in is a valid UserCreatedEvent");

                var result = Task.FromResult(RunEvents(eventType, cancellationToken));
                if (result == null || !result.IsCompletedSuccessfully)
                {
                    // Set CancellationToken to cancel
                    var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                    cancellationToken = cts.Token;
                    cts.Cancel();

                    return Task.FromResult(result);
                }

                return Task.FromResult(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine("--> Could not determine the event");
                Console.WriteLine($"--> Failed to process {ex.Message}");
                return Task.FromException(ex);
            }
        }

        private async Task RunEvents(T published, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Run MediatR Consumers 
            if (typeof(T).IsAssignableFrom(typeof(INotification)))
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Publish(published, cancellationToken);
                }
            }


            // Run regular IoC consumers
            using (var scope = _scopeFactory.CreateScope())
            {
                var simpleConsumers = scope.ServiceProvider.GetServices<IEventConsumer<T>>();

                if (!simpleConsumers.Any())
                {
                    return;
                }

                //Non parallel
                foreach (var simpleConsumer in simpleConsumers)
                {
                    await simpleConsumer.Consume(published, cancellationToken);
                }

                // Another way
                List<Task> myTasks = new List<Task>();

                foreach (var item in simpleConsumers)
                {
                    myTasks.Add(item.Consume(published, cancellationToken));
                }

                await Task.WhenAll(myTasks).ConfigureAwait(false);

                // Run them in parallel
                await Parallel.ForEachAsync(simpleConsumers, async (consumer, cancellationToken) =>
                {
                    var t = Task.Run(() => { consumer.Consume(published, cancellationToken); });

                    await t;
                });

            }
        }
    }
}
