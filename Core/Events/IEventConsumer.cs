namespace Core.Events
{
    public interface IEventConsumer<T>
    {
        Task Consume(T eventToConsume, CancellationToken cancellationToken);
    }
}
