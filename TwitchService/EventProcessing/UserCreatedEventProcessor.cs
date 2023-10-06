using Core.Events;

namespace TwitchService.EventProcessing
{
    public class UserCreatedEventProcessor : EventProcessor<UserCreatedEvent>
    {
        public UserCreatedEventProcessor(IServiceScopeFactory scopeFactory) : base(scopeFactory)
        {
        }
    }
}
