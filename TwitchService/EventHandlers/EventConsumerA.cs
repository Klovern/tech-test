using Core.Events;

namespace TwitchService.EventHandlers
{
    public class EventConsumerA : IEventConsumer<UserCreatedEvent>
    {
        public Task Consume(UserCreatedEvent eventToConsume, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Console.WriteLine("Running EventConsumerA A stuff, good for Open Closed Principle");
            Thread.Sleep(1000);
            return Task.CompletedTask;
        }
    }
}
