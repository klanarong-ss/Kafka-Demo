using Confluent.Kafka;

namespace KafkaConsumer.Services
{
    public interface IConsumeMessage
    {
        void ReadMessage();
    }
    public class ConsumeMessage: IConsumeMessage
    {
        public ConsumeMessage()
        {

        }

        public void ReadMessage()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "188.166.210.217:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                ClientId = "my-app",
                GroupId = "my-group",
                BrokerAddressFamily = BrokerAddressFamily.V4,
            };

            using
            var consumer = new ConsumerBuilder<Ignore,
                string>(config).Build();
            consumer.Subscribe("my-topic");
            try
            {
                while (true)
                {
                    var consumeResult = consumer.Consume();
                    Console.WriteLine($"Message received {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");
                }
            } catch (OperationCanceledException) {
                // The consumer was stopped via cancellation token.
            } finally {
                consumer.Close();
            }
            Console.ReadLine();
        }

    }
}
