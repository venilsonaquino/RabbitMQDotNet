using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace _05_pub_sub;

public class Producer
{
  public static void Run()
  {
    var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "admin", Port = 5672 };

    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();
    var exchange = "amq.fanout";

    var random = new Random();

    for (int i = 0; i < 10; i++)
    {
      var product = new
      {
        id = i,
        name = $"Product {i}",
        price = random.Next(100, 1000) // preço entre 100 e 1000
      };

      var jsonMessage = JsonSerializer.Serialize(product);
      var body = Encoding.UTF8.GetBytes(jsonMessage);

      var props = channel.CreateBasicProperties();
      props.ContentType = "application/json";

      // Publica mensagem no RabbitMQ
      channel.BasicPublish(
          exchange: exchange,
          routingKey: "", // fanout ignora routingKey
          basicProperties: props,
          body: body
      );
    }

    Thread.Sleep(500);
    connection.Close();
    channel.Close();
  }
}
