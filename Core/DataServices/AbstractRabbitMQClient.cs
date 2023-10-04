using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Core.DataServices
{
    public abstract class AbstractRabbitMQClient
    {
        
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConfiguration _configuration;

        public AbstractRabbitMQClient(IConfiguration configuration, string exchange, string exchangeType)
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
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("--> Connected to MessageBus");
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

        public IModel GetChannel()
        {
            return this._channel;
        }

        public IConfiguration GetConfiguration()
        {
            return this._configuration;
        }

        public void Dispose()
        {
            Console.WriteLine("MessageBus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ Connection Shutdown");
        }
    }
}
