using SupportNotification.Ws.Models;

namespace SupportNotification.Ws.Interfaces
{
    public interface ITicketService
    {
        TicketRequest? GetTicket();

        void DeleteJsonTicket(int id);
    }
}