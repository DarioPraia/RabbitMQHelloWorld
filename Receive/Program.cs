using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() {
    HostName = "127.0.0.1",
    Port = 5672, //Protocols.DefaultProtocol.DefaultPort
    UserName = "admin",
    Password = "admin",
};

using (var connection = factory.CreateConnection())

using (var channel = connection.CreateModel()) {
    channel.QueueDeclare(
        queue: "hello",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null
    );

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) => {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($" [x] Received {message}");
    };

    channel.BasicConsume(
        queue: "hello",
        autoAck: true,
        consumer: consumer
    );

    Console.ReadLine();
}