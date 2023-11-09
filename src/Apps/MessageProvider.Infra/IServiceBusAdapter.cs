namespace MessageProvider.Infra;

public interface IServiceBusAdapter
{ 
    Task Send(string messageContent);
}