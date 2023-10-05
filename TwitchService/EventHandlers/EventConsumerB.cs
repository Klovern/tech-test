using Core.Events;

namespace TwitchService.EventHandlers
{
    public class EventConsumerB : IEventConsumer<UserCreatedEvent>
    {
        public Task Consume(UserCreatedEvent eventToConsume, CancellationToken token)
        {
            Console.WriteLine("Running EventConsumerA B stuff, good for Open Closed Principle");
            Thread.Sleep(1000);
            return Task.CompletedTask;
        }
    }
}
