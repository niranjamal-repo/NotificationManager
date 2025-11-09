using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationManager.Consumers;
using NotificationManager.Models;
using NotificationManager.Services;

internal class Program
{
	private static async Task Main(string[] args)
	{
		var host = Host.CreateDefaultBuilder(args)
			.ConfigureAppConfiguration((context, config) =>
			{
				config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
			})
			.ConfigureServices((context, services) =>
			{
				services.Configure<SmsSettings>(context.Configuration.GetSection("SmsSettings"));
				services.Configure<EmailSettings>(context.Configuration.GetSection("EmailSettings"));

				services.AddTransient<IMessageService, EmailMessageService>();
				services.AddTransient<IMessageService, SmsMessageService>();
				services.AddTransient<NotificationConsumer>();
			})
			.Build();

		var consumer = host.Services.GetRequiredService<NotificationConsumer>();

		await consumer.SendAsync(new Message
		{
			Channel = MessageChannel.Email,
			To = "recipient@example.com",
			Subject = "Welcome",
			Body = "Hello from DI-configured Email service!"
		});

		await consumer.SendAsync(new Message
		{
			Channel = MessageChannel.Sms,
			To = "+15555555555",
			Body = "Hello from DI-configured SMS service!"
		});

		await host.StopAsync();
	}
}
