using System.Text;
using System.Text.Json;
using Products.DTOs;
using RabbitMQ.Client;

namespace Products.AsyncDataService
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration=configuration;
            var factory=new ConnectionFactory(){HostName=_configuration["RabbitMQHost"], Password = _configuration["Password"], UserName=_configuration["RabbitMQ:UserName"], VirtualHost="/"};
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange:"trigger",type: ExchangeType.Direct);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutDown;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("--> could not conntect to message bus");
            }
        }        
        public void PublishNewProduct(ProductPublishedDto productPublishedDto)
        {
            var message = JsonSerializer.Serialize(productPublishedDto);
            if(_connection.IsOpen)
            {
                Sendmessage(message);
                Console.WriteLine("--> RaqbbitMq Connection open , Sending Message ...");
            }
            else
            {
                Console.WriteLine("--> Rabbitmq Connection Close ,Not Sending");
            }
        }
        private void Sendmessage(string message)
        {
            var body=Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange:"trigger",routingKey: "",basicProperties: null,body:body);
            Console.WriteLine($"--> We have Sent {message}");
        }
        private void RabbitMQ_ConnectionShutDown(object sender,ShutdownEventArgs e)
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
            Console.WriteLine("--> RabbitMQ ShutDown");
        }
    }
}