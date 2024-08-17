using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System.Text;


var factory = new ConnectionFactory()
{
    //El puerto está por defecto
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();


channel.QueueDeclare(queue: "COLA_CARRITO", durable: true, exclusive: false, autoDelete: false, arguments: null);


//DECLARAMOS EL CONSUMIDOR, no busca nada, sino cuando se notifica que hubo un ingreso a la cola
//toma los elementos y los lee, está escuchando, cuando hay un cambio ahi toma los datos
var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    //Obtenemos el body
    var body = ea.Body.ToArray();

    //Decodificamos el mensaje del body
    var message = Encoding.UTF8.GetString(body);


    // Deserialize the JSON message
    dynamic data = JsonConvert.DeserializeObject(message);


    // Process cart data
    Console.WriteLine($"Received message: {data}");


    // ...Here you would perform your actions with the cart data...



    channel.BasicAck(ea.DeliveryTag, false);
};

//Consumimos el mensaje
channel.BasicConsume(queue: "COLA_CARRITO", autoAck: false, consumer: consumer);


Console.WriteLine("Press any key to exit");
Console.ReadKey();