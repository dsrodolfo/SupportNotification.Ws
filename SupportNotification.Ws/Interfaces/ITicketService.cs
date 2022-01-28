using SupportNotification.Ws.Models.Requests;

namespace SupportNotification.Ws.Interfaces
{
    public interface ITicketService
    {
        TicketRequest? GetTicket();

        bool DeleteJsonTicket(int id);
    }
}