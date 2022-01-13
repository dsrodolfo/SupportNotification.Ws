using Newtonsoft.Json;
using SupportNotification.Ws.Interfaces;
using SupportNotification.Ws.Models;

namespace SupportNotification.Ws.Services
{
    public class TicketService : ITicketService
    {
        private readonly WorkerSettings _workerSettings;

        public TicketService(WorkerSettings workerSettings)
        {
            _workerSettings = workerSettings;
        }

        public TicketRequest? GetTicket()
        {
            string[] files = Directory.GetFiles(_workerSettings.NewTicketsDirectory);

            if (files.Any())
            {
                using StreamReader reader = new StreamReader(files[0]);
                string json = reader.ReadToEnd();
                var ticket = JsonConvert.DeserializeObject<TicketRequest>(json);

                return ticket;
            }

            return null;
        }

        public void DeleteJsonTicket(int id)
        {
            string[] files = Directory.GetFiles(_workerSettings.NewTicketsDirectory);
            string? path = files.Where(x => x.Contains(id.ToString()))
                                .FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(path))
            {
                File.Delete(path);
            }
        }
    }
}