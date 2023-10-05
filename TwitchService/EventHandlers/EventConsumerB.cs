using Core.Events;

namespace TwitchService.EventHandlers
{
    public class EventConsumerB : IEventConsumer<UserCreatedEvent>
    {
        public Task Consume(UserCreatedEvent eventToConsume, CancellationToken token)
        {
            Console.WriteLine("Doing regular IoC stuff, good for Open Closed Principle");

            return Task.CompletedTask;
        }
    }
}
