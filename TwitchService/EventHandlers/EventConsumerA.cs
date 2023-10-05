using Core.Events;

namespace TwitchService.EventHandlers
{
    public class EventConsumerA : IEventConsumer<UserCreatedEvent>
    {
        public Task Consume(UserCreatedEvent eventToConsume, CancellationToken token)
        {
            Console.WriteLine("Running EventHandler A stuff, good for Open Closed Principle");

            return Task.CompletedTask;
        }
    }
}
