using Core.DataServices;
using Core.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace UserService.DataServices
{
    public class UserServiceClient : AbstractRabbitMQClient, IUserServiceClient
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;

        public UserServiceClient(IConfiguration configuration) : base(configuration, "trigger", "fanout")
        {
            _channel = base.GetChannel();
            _connection = base.GetConnection();
        }

        public void PublishX(string message)
        {
            if (_connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ Connection Open, sending message...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("--> RabbitMQ connectionis closed, not sending");
            }
        }

        private void SendMessage(string message)
        {
            var serializedEvent = JsonSerializer.Serialize(new UserCreatedEvent()
            {
                Id = 49,
                FirstName = "John",
                LastName = "Dough",
                Email = "john.doughe.baker@gmai.com"
            });

            var body = Encoding.UTF8.GetBytes(serializedEvent);

            _channel.BasicPublish(exchange: "trigger",
                            routingKey: "",
                            basicProperties: null,
                            body: body);
            Console.WriteLine($"--> We have sent {message}");
        }
    }
}
