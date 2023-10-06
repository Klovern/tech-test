namespace Core.Events
{
    public interface IEventProcessor<T> where T : notnull
    {
        void ProcessEvent(string eventMessage, CancellationToken cancellationToken);
    }
}
