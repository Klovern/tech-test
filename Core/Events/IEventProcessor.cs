namespace Core.Events
{
    public interface IEventProcessor<T> where T : notnull
    {
        Task ProcessEvent(string eventMessage, CancellationToken cancellationToken);
    }
}
