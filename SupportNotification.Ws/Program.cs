using SupportNotification.Ws;
using SupportNotification.Ws.Interfaces;
using SupportNotification.Ws.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        WorkerSettings settings = configuration.GetSection("Settings").Get<WorkerSettings>();
        services.AddSingleton<IWorkerSettings>(settings);

        services.AddSingleton<ITemplateService, TemplateService>();
        services.AddSingleton<IEmailService, EmailService>();
        services.AddSingleton<ITicketService, TicketService>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();