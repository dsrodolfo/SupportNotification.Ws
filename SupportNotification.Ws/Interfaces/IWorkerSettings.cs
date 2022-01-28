namespace SupportNotification.Ws.Interfaces
{
    public interface IWorkerSettings
    {
        public string? NewTicketsDirectory { get; set; }
        public string? SentEmailsDirectory { get; set; }
    }
}