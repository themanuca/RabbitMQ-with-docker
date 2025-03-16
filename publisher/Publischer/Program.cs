// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

static async Task Main(string[] args)
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
    const string mensagem = "Primeiro Teste com RABBITMQ";

    var body = Encoding.UTF8.GetBytes(mensagem);

    await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "filaTESTE", body: body);
    Console.WriteLine("[*] Enviando a mensagem");
    Console.WriteLine("Pressionone qualquer tecla");
    Console.ReadLine();
}
await Main([]);