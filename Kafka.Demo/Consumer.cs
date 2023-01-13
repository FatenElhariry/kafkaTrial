using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Demo
{
    public static class Consumer
    {
        public static async Task RunConsumer()
        {
            var config = new ConsumerConfig()
            {
                GroupId = "weather-conumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<Null, string>(config).Build();
            consumer.Subscribe("weather-topic");
            CancellationToken token = new();
            var index = 0;
            try
            {
                while (true)
                {
                    var response = consumer.Consume(token);
                    if (response.Message != null)
                    {
                        var weather = JsonConvert.DeserializeObject<Weather>(response.Message.Value);
                        await Console.Out.WriteLineAsync($"[{nameof(Consumer)}] [{++index}]: {weather.ToString()}");
                    }
                }
            }
            catch (Exception ex)
            {

                await Console.Out.WriteLineAsync($"[{nameof(Consumer)}] [error]: {ex.Message}");
            }
        }
    }
}
