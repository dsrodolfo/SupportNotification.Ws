using SupportNotification.Ws.Interfaces;

namespace SupportNotification.Ws
{
    public class WorkerSettings : IWorkerSettings
    {
        public string? NewTicketsDirectory { get; set; }
        public string? SentEmailsDirectory { get; set ; }
    }
}