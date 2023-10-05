using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace Core.DataServices
{
    public abstract class AbstractBackgroundRabbitMQClient : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConfiguration _configuration;
        private readonly string _queueName;

        public AbstractBackgroundRabbitMQClient(IConfiguration configuration, string exchange, string exchangeType, string routing)
        {
            _configuration = configuration;
            try
            {
                if (!ExchangeType.All().Contains(exchangeType))
                {
                    throw new Exception();
                }

                var HostName = _configuration["RabbitMQHost"];
                var Port = int.Parse(_configuration["RabbitMQPort"]);

                var factory = new ConnectionFactory()
                {
                    HostName = _configuration["RabbitMQHost"],
                    Port = int.Parse(_configuration["RabbitMQPort"]),
                    Ssl = new SslOption()
                    {
                        Enabled = false
                    }
                };

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: exchange, type: exchangeType);
                _queueName = _channel.QueueDeclare().QueueName;

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("--> Connected to MessageBus");

                _channel.QueueBind(queue: _queueName,
                    exchange: exchange,
                    routingKey: routing ?? string.Empty);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"--> AbstractRabbitMQClient threw an exception: {ex.Message}");
            }
        }

        public IConnection GetConnection()
        {
            return this._connection;
        }

        public string GetQueueName()
        {
            return this._queueName;
        }

        public IModel GetChannel()
        {
            return this._channel;
        }

        public IConfiguration GetConfiguration()
        {
            return this._configuration;
        }

        public override void Dispose()
        {
            Console.WriteLine("MessageBus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }

            base.Dispose();
        }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ Connection Shutdown");
        }
    }
}
