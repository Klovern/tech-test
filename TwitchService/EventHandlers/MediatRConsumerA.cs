using Core.Events;
using MediatR;

namespace TwitchService.EventHandlers
{
    public class MediatRConsumerA : INotificationHandler<UserCreatedEvent>
    {
        public Task Handle(UserCreatedEvent request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Console.WriteLine("--> Im doing handler A");
            return Task.CompletedTask;
        }
    }
}
