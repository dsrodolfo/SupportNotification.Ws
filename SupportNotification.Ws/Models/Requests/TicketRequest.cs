namespace SupportNotification.Ws.Models.Requests
{
    public class TicketRequest
    {
        public int TicketId { get; set; }
        public string? Title { get; set; }
        public string? ProductName { get; set; }
        public string? CustomerName { get; set; }
        public string? Description { get; set; }
    }
}