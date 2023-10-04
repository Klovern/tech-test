﻿using Core.Events;
using MediatR;

namespace TwitchService.EventHandlers
{
    public class AnotherCreatedRequestHandler : INotificationHandler<UserCreatedEvent>
    {
        public Task Handle(UserCreatedEvent request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Console.WriteLine("--> Im doing handler B");

            return Task.CompletedTask;
        }
    }
}
