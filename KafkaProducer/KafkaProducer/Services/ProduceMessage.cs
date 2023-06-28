using Confluent.Kafka;
using Newtonsoft.Json;

namespace KafkaProducer.Services
{
    public interface IProduceMessage
    {
        Task ProduceAsync();
    }
    public class ProduceMessage : IProduceMessage
    {
        private readonly IProducer<Null, string> producer;

        public ProduceMessage(IProducer<Null, string> producer)
        {
            this.producer = producer;
        }

        public async Task ProduceAsync()
        {
            var temp = Enumerable.Range(1, 1).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
            })
            .ToArray();

            var message = new Message<Null,
                string>
            {
                Value = JsonConvert.SerializeObject(temp),
            };
            var deliveryReport = await producer.ProduceAsync("my-topic", message);
        }

    }
}
