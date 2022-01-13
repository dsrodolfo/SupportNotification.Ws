using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using SupportNotification.Ws.Interfaces;
using SupportNotification.Ws.Models;
using System.Net.Mail;

namespace SupportNotification.Ws.Services
{
    public class EmailService : IEmailService
    {
        private readonly ITemplateService _templateService;
        private readonly WorkerSettings _workerSettings;

        public EmailService(ITemplateService templateService, 
                            WorkerSettings workerSettings)
        {
            _templateService = templateService;
            _workerSettings = workerSettings;
        }

        public async Task<SendResponse> SendEmail(TicketRequest ticketRequest)
        {
            Email.DefaultSender = GetSmtpSender();
            Email.DefaultRenderer = new RazorRenderer();

            var sendResponse = await Email
                .From("supportnotification@mycompany.com")
                .To("employees.it@mycompany.com")
                .Subject($"New Ticket - Protocol: {ticketRequest.TicketId} - Title: {ticketRequest.Title}")
                .UsingTemplate(_templateService.GetDefaultTemplate(), ticketRequest)
                .SendAsync();

            return sendResponse;
        }

        private SmtpSender GetSmtpSender()
        {
            SmtpSender sender = new(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = _workerSettings.SentEmailsDirectory
            });

            return  sender;
        }
    }
}