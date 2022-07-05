using System.Text;
using Products.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Products.AsyncDataService
{
    public class MessageBusSubScriber : BackgroundService
    {
        private IConfiguration _configuration;
        private IEventProcessor _eventprocessor;        
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public MessageBusSubScriber(IConfiguration configuration,IEventProcessor eventprocessor)
        {
            _configuration=configuration;
            _eventprocessor=eventprocessor;
            InitializeRabbitMQ();
            
        }
        private void InitializeRabbitMQ()
        {
            var factory=new ConnectionFactory(){HostName="localhost", Password = "moein.1379", UserName="guest", VirtualHost="/"};
            _connection=factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare("trigger",ExchangeType.Direct);
            _queueName=_channel.QueueDeclare().QueueName;
            _channel.QueueBind(_queueName,"trigger","",null);
            Console.WriteLine("--> Listening On Message bus ...");

            _connection.ConnectionShutdown+=RabbiMQ_ConnectionShutDown;

        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received+=(ModuleHandle,ea)=>
            {                
                Console.WriteLine("--> Event Received");
                var body =ea.Body;
                var notifcationmessage = Encoding.UTF8.GetString(body.ToArray());
                _eventprocessor.ProcessEvent(notifcationmessage);
            };
            _channel.BasicConsume(_queueName,true,consumer);
            return Task.CompletedTask;
        }
        private void RabbiMQ_ConnectionShutDown(object? sender, ShutdownEventArgs e)
        {
            Dispose();
            Console.WriteLine("--> Connection ShutDown");
        }
        public override void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
            base.Dispose();
        }
    }
}