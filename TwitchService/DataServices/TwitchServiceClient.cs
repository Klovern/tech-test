using Core.DataServices;
using Core.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TwitchService.EventProcessing;

namespace TwitchService.DataServices
{
    public class TwitchServiceClient : AbstractBackgroundRabbitMQClient
    {

        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly IEventProcessor<UserCreatedEvent> _eventProcessor;
        private readonly string _queueName;

        public TwitchServiceClient(IConfiguration configuration, IEventProcessor<UserCreatedEvent> eventProcessor) : base(configuration, "trigger", "fanout", string.Empty)
        {
            _channel = base.GetChannel();
            _connection = base.GetConnection();
            _queueName = base.GetQueueName();

            _eventProcessor = eventProcessor;
            Console.WriteLine("--> Listening on the Message Bus...");
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, ea) =>
            {
                Console.WriteLine("--> Event Received!");

                var body = ea.Body;
                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(notificationMessage, stoppingToken);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
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
