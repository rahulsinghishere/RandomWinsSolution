namespace RandomWins.API.BackgroundServices
{
    public interface IMessageQueueService
    {
        Task SendMessageAsync(string message);
        Task ReceiveMessageAsync();
    }
}
