using RabbitMQ.Client;

var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "admin", Port = 5672 };

using var connection = await factory.CreateConnectionAsync();
Console.WriteLine("Conectado ao RabbitMQ com sucesso!");
using var channel = await connection.CreateChannelAsync();
Console.WriteLine("Canal criado com sucesso!");

await Task.Delay(30000);

await connection.CloseAsync();
await channel.CloseAsync();

Console.WriteLine("Conexão fechada com sucesso!");