using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotificationManager.Models;

namespace NotificationManager.Services
{
    public class SmsMessageService : IMessageService
    {
        private readonly SmsSettings _settings;
        private readonly ILogger<SmsMessageService> _logger;

        public SmsMessageService(IOptions<SmsSettings> settings, ILogger<SmsMessageService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public bool CanHandle(MessageChannel channel) => channel == MessageChannel.Sms;

        public Task SendAsync(Message message, CancellationToken cancellationToken = default)
        {
            if (!CanHandle(message.Channel))
            {
                throw new InvalidOperationException("SmsMessageService cannot handle this channel.");
            }

            _logger.LogInformation(
                "Sending SMS via {Provider} from {From} to {To}. Body: {Body}",
                _settings.Provider,
                _settings.FromNumber,
                message.To,
                message.Body);

            return Task.CompletedTask;
        }
    }
}
