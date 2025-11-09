using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NotificationManager.Models;
using NotificationManager.Services;

namespace NotificationManager.Consumers
{
    public class NotificationConsumer
    {
        private readonly IEnumerable<IMessageService> _messageServices;
        private readonly ILogger<NotificationConsumer> _logger;

        public NotificationConsumer(IEnumerable<IMessageService> messageServices, ILogger<NotificationConsumer> logger)
        {
            _messageServices = messageServices;
            _logger = logger;
        }

        public async Task SendAsync(Message message, CancellationToken cancellationToken = default)
        {
            var service = _messageServices.FirstOrDefault(s => s.CanHandle(message.Channel));
            if (service == null)
            {
                throw new InvalidOperationException($"No IMessageService registered for channel {message.Channel}.");
            }

            await service.SendAsync(message, cancellationToken);
        }
    }
}
