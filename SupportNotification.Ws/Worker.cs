using SupportNotification.Ws.Interfaces;
using SupportNotification.Ws.Models;

namespace SupportNotification.Ws
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITicketService _ticketService;
        private readonly IEmailService _emailService;

        public Worker(ILogger<Worker> logger, 
                      ITicketService ticketService, 
                      IEmailService emailService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    TicketRequest? ticketRequest = _ticketService.GetTicket();

                    if (ticketRequest != null)
                    {
                        _logger.LogInformation($"Working on Ticket Id: {ticketRequest.TicketId}", DateTimeOffset.Now);
                        var sendResponse = await _emailService.SendEmail(ticketRequest);

                        if (sendResponse.Successful)
                        {
                            _logger.LogInformation("Email sent at: {time}", DateTimeOffset.Now);
                            _ticketService.DeleteJsonTicket(ticketRequest.TicketId);
                            _logger.LogInformation("JSON Ticket deleted", DateTimeOffset.Now);
                        }
                        else
                        {
                            _logger.LogError("Impossible to send an e-mail", DateTimeOffset.Now);
                        }
                    }
                    else
                    {
                        _logger.LogInformation("The folder is empty", DateTimeOffset.Now);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops!Something went wrong!", DateTimeOffset.Now);
                }
                finally
                {
                    await Task.Delay(1000, stoppingToken);
                }              
            }
        }
    }
}