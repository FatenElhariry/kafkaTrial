using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Demo
{
    public static class Producer
    {
        public static async Task BuildProducer()
        {
            var config = new ProducerConfig() { BootstrapServers = "localhost:9092" };
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            try
            {
                string? state;
                var index = 0;
                
                while (!string.IsNullOrEmpty((state = await Console.In.ReadLineAsync())))
                {
                    var response = await producer.ProduceAsync("weather-topic", new Message<Null, string>()
                    {
                        Value = JsonConvert.SerializeObject(new Weather(state, 70))
                    });
                    await Console.Out.WriteLineAsync($"[{nameof(Producer)}][ {++index}]: {response.Message}");
                    Thread.Sleep(5000);
                }
            }
            catch (ProduceException<Null, string> ex)
            {
                await Console.Out.WriteLineAsync($"[{nameof(Producer)}] [Kafka Error]:: {ex.Message}");
            }
            catch (Exception ex) {
                await Console.Out.WriteLineAsync($"[{nameof(Producer)}] [error]: {ex.Message}");
            }
        }
    }
}
