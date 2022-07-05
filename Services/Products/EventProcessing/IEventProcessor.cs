namespace Products.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);

    }
}