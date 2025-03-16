// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

static async Task Main()
{
    var factory = new ConnectionFactory()
    {
        HostName = "localhost",
        UserName = "guest",
        Password = "guest"
    };

    using var connection = await factory.CreateConnectionAsync();
    using var channel = await connection.CreateChannelAsync();

    await channel.QueueDeclareAsync(queue: "filaTESTE", durable: false, exclusive: false, autoDelete: false, arguments: null);
    Console.WriteLine("[*]Aguardando mensagem");
    var consumer = new AsyncEventingBasicConsumer(channel);
    consumer.ReceivedAsync += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var mensagem = Encoding.UTF8.GetString(body);
        Console.WriteLine($"[x] Recebi:{mensagem}");
        return Task.CompletedTask;

    };

    await channel.BasicConsumeAsync(queue: "filaTESTE", autoAck:true, consumer:consumer);
    Console.WriteLine("Pressione qualquer tecla");
    Console.ReadLine();
}
await Main();