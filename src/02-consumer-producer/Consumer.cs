using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace _02_consumer_producer;

public class Consumer
{
  public static async Task Run()
  {
    var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "admin", Port = 5672 };

    using var connection = await factory.CreateConnectionAsync();
    using var channel = await connection.CreateChannelAsync();

    var queue = "hello";

    await channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false);

    Console.WriteLine(" [*] Esperando mensagens. Para sair pressione [enter]");

    var consumer = new AsyncEventingBasicConsumer(channel);
    consumer.ReceivedAsync += async (model, ea) =>
    {
      var body = ea.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);
      Console.WriteLine($" [x] Recebido {message}");

      await Task.Delay(10);
    };

    await channel.BasicConsumeAsync(queue: queue, autoAck: true, consumer: consumer);

    Console.WriteLine("Pressione [enter] para sair.");
    Console.ReadLine();
  }
}
