﻿using System;
using System.Net.Http.Headers;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    public class Sender
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory(){HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("BasicTest", false, false, false, null);

                string message = "Getting started RabbitMQ";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("","BasicTest",null,body);
                Console.WriteLine("Sent message {0}...", message);
            }

            Console.WriteLine("Press [enter] to exit sender app");
            Console.ReadLine();
        }
    }
}
