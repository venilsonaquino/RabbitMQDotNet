using RabbitMQ.Client;
using System.Text;

public class Producer
{
  public static async Task Run()
  {
    var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "admin", Port = 5672 };

    using var connection = await factory.CreateConnectionAsync();
    using var channel = await connection.CreateChannelAsync();

    var queue = "hello";
    var message = "Hello World!";

    await channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false);
    await channel.BasicPublishAsync(exchange: "", routingKey: queue, body: Encoding.UTF8.GetBytes(message));

    Console.WriteLine($"Mensagem enviada para a fila {queue}");

    await Task.Delay(500);

    await connection.CloseAsync();
    await channel.CloseAsync();
  }
}