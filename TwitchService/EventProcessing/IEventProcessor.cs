namespace TwitchService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}
