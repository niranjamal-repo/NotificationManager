using System.Threading;
using System.Threading.Tasks;
using NotificationManager.Models;

namespace NotificationManager.Services
{
    public interface IMessageService
    {
        bool CanHandle(MessageChannel channel);
        Task SendAsync(Message message, CancellationToken cancellationToken = default);
    }
}
