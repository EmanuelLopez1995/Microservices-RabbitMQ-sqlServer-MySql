using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using TiendaServicio.Api.CarritoCompra.Modelo;

namespace TiendaServicio.Api.CarritoCompra.RabbitMq
{
    public class Cola
    {
        private readonly IConfiguration _configuration;
        public Cola(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private void Send(string mensaje)
        {
            string host = _configuration["UrlRabbitMq"];
            string user = _configuration["UserRabbitMq"].ToString();
            string pass = _configuration["PassRabbitMq"].ToString();
            string nombreCola = _configuration["ColaRabbitMq"].ToString();

            //create the connection
            var factory = new ConnectionFactory()
            {
                HostName = host,
                UserName = user,
                Password = pass,
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: nombreCola,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(mensaje);

                channel.BasicPublish(exchange: "",
                     routingKey: nombreCola,
                     basicProperties: null,
                     body: body);

                string mensajeLogs = String.Format("End of sending to queue {0} -- User: {1}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), mensaje);

                Console.WriteLine(mensajeLogs);

            }

        }


        public async Task ProcesarAsync(CarritoSesion carritoSesion)
        {
            await Task.Delay(2000);
            try
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                // we serialize
                string mensaje = JsonConvert.SerializeObject(carritoSesion, jsonSettings);
                string mensajeLogs = String.Format("Start sending to queue {0} -- User: {1}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), mensaje);
                Console.WriteLine(mensajeLogs);
                Send (mensaje);
                // we send
                //var cola = new Cola(_configuration);
                //cola.Enviar(mensaje);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
