using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace _05_pub_sub;

public class Consumer
{
  public static void Run()
  {
    var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "admin", Port = 5672 };

    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();

    var queue = "hello";

    channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);

    Console.WriteLine(" [*] Esperando mensagens. Para sair pressione [enter]");

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
      var body = ea.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);
      Console.WriteLine($" [x] Recebido {message}");
      Thread.Sleep(10);
    };

    channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);

    Console.WriteLine("Pressione [enter] para sair.");
    Console.ReadLine();
  }
}
