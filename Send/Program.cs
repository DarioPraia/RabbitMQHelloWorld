using System;
using RabbitMQ.Client;
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

    string message = "Hello World!";
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange: "",
        routingKey: "hello",
        basicProperties: null,
        body: body
    );

    Console.WriteLine($" [x] Sent {message}");
}

// Console.WriteLine($" [x] Sent {0}", message);
Console.ReadLine();