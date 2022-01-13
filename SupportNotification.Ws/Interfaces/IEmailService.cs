using FluentEmail.Core.Models;
using SupportNotification.Ws.Models;

namespace SupportNotification.Ws.Interfaces
{
    public interface IEmailService
    {
        Task<SendResponse> SendEmail(TicketRequest ticketRequest);
    }
}