using System;
using Microsoft.Extensions.Logging;


namespace ECommerce.Application.Notifications;

public interface INotificationChannel
{
    string Name { get; }
    Task SendAsync(string to, string message);
}

public class EmailChannel(ILogger<EmailChannel> logger) : INotificationChannel
{
    public string Name => "E-Posta";

    public async Task SendAsync(string to, string message)
    {
        await Task.CompletedTask;
        logger.LogInformation("[{Channel}] {To}: {Message} gönderildi", Name, to, message);
    }

}

public class SMSChannel(ILogger<EmailChannel> logger) : INotificationChannel
{
    public string Name => "SMS";

    public async Task SendAsync(string to, string message)
    {
        await Task.CompletedTask;
        logger.LogInformation("[{Channel}] {To}: {Message} gönderildi", Name, to, message);
    }

}

public class WhatsAppChannel(ILogger<EmailChannel> logger) : INotificationChannel
{
    public string Name => "WhatsApp";

    public async Task SendAsync(string to, string message)
    {
        await Task.CompletedTask;
        logger.LogInformation("[{Channel}] {To}: {Message} gönderildi", Name, to, message);
    }

}
