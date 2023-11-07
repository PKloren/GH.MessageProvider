namespace MessageProvider.Infra;

public interface IServiceBusAdapter
{ 
    Task Send(string team, string messageContent);
}