namespace MessageProvider.Infra;

public class MessageServiceOptions
{
    public const string Name = nameof(MessageServiceOptions);

    public string ConnectionString { get; set; } = string.Empty;

    public string QueueName { get; set; } = string.Empty;
}