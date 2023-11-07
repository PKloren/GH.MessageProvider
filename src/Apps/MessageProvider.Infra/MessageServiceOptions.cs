namespace MessageProvider.Infra;

public class MessageServiceOptions
{
    public const string Name = nameof(MessageServiceOptions);

    public string ConnectionString { get; set; } = string.Empty;

    public string QueueNamePrefix { get; set; } = string.Empty;
}