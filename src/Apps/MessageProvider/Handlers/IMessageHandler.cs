using MessageProvider.Models;

namespace MessageProvider.Handlers;

public interface IMessageHandler
{
    Task PostMessage(JobViewModel job);
}