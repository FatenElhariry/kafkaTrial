// this demo simplify the usage of kafka consumer 

using Kafka.Demo;

var t1 =  Task.Run(Producer.BuildProducer);

var t2 = Task.Run(Consumer.RunConsumer);
Task.WaitAll(t1, t2);
Console.ReadLine();

