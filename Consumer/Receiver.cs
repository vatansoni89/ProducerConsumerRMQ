using RabbitMQ.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Channels;
using RabbitMQ.Client.Events;

namespace Consumer
{
    class Receiver
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("BasicTest", false, false, false, null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Received Message {0}...",message);
                };

                channel.BasicConsume("BasicTest",true,consumer);
                Console.WriteLine("Press [enter] to exit Consumer...");
                Console.ReadLine();
            }
        }
    }
}
