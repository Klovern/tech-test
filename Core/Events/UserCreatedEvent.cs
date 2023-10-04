using MediatR;

namespace Core.Events
{
    public class UserCreatedEvent : INotification
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
