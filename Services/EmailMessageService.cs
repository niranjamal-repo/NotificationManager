using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotificationManager.Models;

namespace NotificationManager.Services
{
    public class EmailMessageService : IMessageService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailMessageService> _logger;

        public EmailMessageService(IOptions<EmailSettings> settings, ILogger<EmailMessageService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public bool CanHandle(MessageChannel channel) => channel == MessageChannel.Email;

        public Task SendAsync(Message message, CancellationToken cancellationToken = default)
        {
            if (!CanHandle(message.Channel))
            {
                throw new InvalidOperationException("EmailMessageService cannot handle this channel.");
            }

            _logger.LogInformation(
                "Sending EMAIL via {Host}:{Port} as {User} from {From} to {To}. Subject: {Subject}. Body: {Body}",
                _settings.SmtpHost,
                _settings.SmtpPort,
                _settings.Username,
                _settings.FromAddress,
                message.To,
                message.Subject ?? string.Empty,
                message.Body);

            return Task.CompletedTask;
        }
    }
}
