using Core.Events;
using MediatR;
using System.Text.Json;

namespace TwitchService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {

        private readonly IServiceScopeFactory _scopeFactory;

        public EventProcessor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void ProcessEvent(string message)
        {
            DetermineEvent(message);
        }

        private void DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            try
            {
                var eventType = JsonSerializer.Deserialize<UserCreatedEvent>(notifcationMessage);
                Console.WriteLine("---> Message passed in is a valid UserCreatedEvent");

                if (eventType != null)
                {
                    RunEvents(eventType, new CancellationToken());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("---> Maybe this is wrong way of doing it");
            }
        }

        private async void RunEvents(UserCreatedEvent published, CancellationToken cancellationToken)
        {
            // Run MediatR Consumers 
            using (var scope = _scopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Publish(published, cancellationToken);
            }

            // Run regular IoC consumers
            using (var scope = _scopeFactory.CreateScope())
            {
                var simpleConsumers = scope.ServiceProvider.GetServices<IEventConsumer<UserCreatedEvent>>();

                //Non Paralell
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


                // Run them in paralell
                var options = new ParallelOptions() { MaxDegreeOfParallelism = 20 };
                await Parallel.ForEachAsync(simpleConsumers, options, async (consumer, cancellationToken) =>
                {
                    var t = Task.Run(() => { consumer.Consume(published, cancellationToken); });

                    await t;
                });

            }
        }
    }
}
