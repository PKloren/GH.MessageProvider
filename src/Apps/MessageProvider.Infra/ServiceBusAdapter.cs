using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;

namespace MessageProvider.Infra;

public class ServiceBusAdapter : IServiceBusAdapter
{
    private readonly IOptions<MessageServiceOptions> _options;

    public ServiceBusAdapter(IOptions<MessageServiceOptions> options)
    {
        _options = options;
    }

    public async Task Send(string team, string messageContent)
    {
        var queueName = $"{_options.Value.QueueNamePrefix}{team}";
        await using var client = new ServiceBusClient(_options.Value.ConnectionString);
        var sender = client.CreateSender(queueName);
        await sender.SendMessageAsync(new ServiceBusMessage(messageContent));
    }
}