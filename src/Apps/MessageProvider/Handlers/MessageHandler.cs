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
                    await _serviceBusAdapter.Send(job.Team, content);
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
                Guid.TryParse(job.SubjectId, out id);
            }

            Randomizer.Seed = new Random(id.GetHashCode());

            var faker = new Faker<BusMessage>()
                .RuleFor(x => x.AanvragerKey, f => id)
                .RuleFor(x => x.BerichtType, f => job.MessageType)
                .RuleFor(x => x.KgbVariant, f => f.PickRandom(true, false))
                .RuleFor(x => x.DatumDagtekening, f => f.Date.Between(DateTime.Today, DateTime.Today.AddDays(14)))
                .RuleFor(x => x.Toeslagjaar, DateTime.Today.Year + 1);

            var item = faker.Generate(1);

            item[0].ReactieDatum = item[0].DatumDagtekening.AddDays(14);

            return item[0];
        }
    }
}
