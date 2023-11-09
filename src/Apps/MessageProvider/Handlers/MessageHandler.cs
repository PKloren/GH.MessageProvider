using Bogus;
using MessageProvider.Infra;
using MessageProvider.Models;

namespace MessageProvider.Handlers
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IServiceBusAdapter _serviceBusAdapter;

        public MessageHandler(IServiceBusAdapter serviceBusAdapter)
        {
            _serviceBusAdapter = serviceBusAdapter;
        }

        public async Task PostMessage(JobViewModel job)
        {
            for (var i = 0; i < job.MessageCount; i++)
            {
                var msg = GetBusMessage(job);

                var content = msg.ToString();

                if (content != string.Empty)
                {
                    await _serviceBusAdapter.Send(content);
                }

                if (job.SubjectId != null)
                    break;
            }
        }

        private BusMessage GetBusMessage(JobViewModel job)
        {
            var id = Guid.NewGuid();
            if (job.SubjectId != null)
            {
                Guid.TryParse(job.SubjectId, out var newId);
                if (newId != Guid.Empty)
                    id = newId;
            }

            Randomizer.Seed = new Random(id.GetHashCode());

            var faker = new Faker<BusMessage>()
                .RuleFor(x => x.KgbVariant, f => f.PickRandom(true, false))
                .RuleFor(x => x.DatumDagtekening, f => f.Date.Between(DateTime.Today, DateTime.Today.AddDays(14)))
                .RuleFor(x => x.Toeslagjaar, DateTime.Today.Year + 1);

            var message = faker.Generate(1)[0];
            message.ReactieDatum = message.DatumDagtekening.AddDays(14);
            message.BerichtType = job.MessageType;
            message.AanvragerKey = id;

            return message;
        }
    }
}
