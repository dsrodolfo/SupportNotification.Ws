using Newtonsoft.Json;
using SupportNotification.Ws.Interfaces;
using SupportNotification.Ws.Models.Requests;

namespace SupportNotification.Ws.Services
{
    public class TicketService : ITicketService
    {
        private readonly IWorkerSettings _workerSettings;

        public TicketService(IWorkerSettings workerSettings)
        {
            _workerSettings = workerSettings;
        }

        public TicketRequest? GetTicket()
        {
            if (!string.IsNullOrWhiteSpace(_workerSettings.NewTicketsDirectory))
            {
                string[] files = Directory.GetFiles(_workerSettings.NewTicketsDirectory);

                if (files.Any())
                {
                    using StreamReader reader = new StreamReader(files[0]);
                    string json = reader.ReadToEnd();
                    var ticket = JsonConvert.DeserializeObject<TicketRequest>(json);

                    return ticket;
                }
            }

            return null;
        }

        public bool DeleteJsonTicket(int id)
        {
            if (!string.IsNullOrWhiteSpace(_workerSettings.NewTicketsDirectory))
            {
                string[] files = Directory.GetFiles(_workerSettings.NewTicketsDirectory);
                string? path = files.Where(x => x.Contains(id.ToString()))
                                    .FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(path))
                {
                    File.Delete(path);

                    return true;
                }

                return false;
            }

            return false;
        }
    }
}