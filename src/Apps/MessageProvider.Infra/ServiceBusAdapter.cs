using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MessageProvider.Infra;

public class ServiceBusAdapter : IServiceBusAdapter
{
    private readonly IOptions<MessageServiceOptions> _options;
    private readonly ILogger<ServiceBusAdapter> _logger;

    public ServiceBusAdapter(ILogger<ServiceBusAdapter> logger, IOptions<MessageServiceOptions> options)
    {
        _options = options;
        _logger = logger;
    }

    public async Task Send(string messageContent)
    {
        _logger.LogWarning($"prefix: {_options.Value.QueueName}");
        _logger.LogWarning($"connection: {_options.Value.ConnectionString}");
        var queueName = $"{_options.Value.QueueName}";
        await using var client = new ServiceBusClient(_options.Value.ConnectionString, new DefaultAzureCredential());
        var sender = client.CreateSender(queueName);
        await sender.SendMessageAsync(new ServiceBusMessage(messageContent));
    }
}