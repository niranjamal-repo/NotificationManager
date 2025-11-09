# NotificationManager

A .NET 8 console application demonstrating dependency injection driven notifications for email and SMS channels. Services are registered using `Microsoft.Extensions.Hosting`, settings are pulled from `appsettings.json`, and a `NotificationConsumer` routes messages to the correct provider via an `IMessageService` abstraction.

## Features
- Email and SMS notification services implementing a shared `IMessageService` interface
- Strongly typed configuration binding (`EmailSettings`, `SmsSettings`) via `IOptions<T>`
- Dependency Injection setup with `Host.CreateDefaultBuilder`
- Simple consumer that selects the appropriate service based on `MessageChannel`
- Logging placeholders where actual provider integrations can be added

## Project Structure
- `Program.cs`: Builds the host, configures services/options, and demonstrates sending messages
- `Models/`: Message models and configuration POCOs (`Message`, `EmailSettings`, `SmsSettings`)
- `Services/`: `IMessageService` contract plus email/SMS implementations
- `Consumers/NotificationConsumer.cs`: Resolves the appropriate message service at runtime
- `appsettings.json`: Stores provider configuration (replace with your real credentials)

## Prerequisites
- .NET SDK 8.0 or higher

## Getting Started
```bash
# restore & build
 dotnet build

# run the console app
 dotnet run
```

Update `appsettings.json` with real SMTP and SMS provider credentials before running in production environments.

## Extending
Add new channels by:
1. Extending the `MessageChannel` enum
2. Creating a new settings class & configuration section
3. Implementing `IMessageService` for the new channel
4. Registering the implementation in `Program.cs`

## License
MIT
