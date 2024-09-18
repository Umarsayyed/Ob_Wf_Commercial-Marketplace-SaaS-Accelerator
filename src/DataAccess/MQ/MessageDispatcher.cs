using Marketplace.SaaS.Accelerator.DataAccess.DataModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace Marketplace.SaaS.Accelerator.DataAccess.MQ
{
    public static class MessageDispatcher
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly IConfiguration _configuration;       

        #region PubSub Event        
        public static void Dispatch(PubSubEventModel pubSubEventModel)
        {
            logger.Info($"Start MessageDispatcher, Dispatch(pubSubEventModel)");
            var exchange = QueueAndExchange.Exchange.PubSub.Exchange;
            logger.Info($"MessageDispatcher, Dispatch(pubSubEventModel), RabbitMQ:PubSubExchange={exchange}");
            if (string.IsNullOrWhiteSpace(exchange))
                throw new ArgumentNullException("PubSubExchange name not provided");
            string serializedMessage = JsonSerializer.Serialize(pubSubEventModel);
            Dispatch(serializedMessage, exchange, ExchangeType.Fanout);
            logger.Info($"End MessageDispatcher, Dispatch(pubSubEventModel)");
        }
        #endregion       
        private static void Dispatch(string message, string exchange, string exchangeType, string routingKey = "")
        {
            var hostName = ConfigurationReader.Instance.Configuration.GetSection("RabbitMQ:HostName").Value;
            var virtualHost = ConfigurationReader.Instance.Configuration.GetSection("RabbitMQ:VirtualHost").Value;
            var port = ConfigurationReader.Instance.Configuration.GetSection("RabbitMQ:Port").Value;
            var userName = ConfigurationReader.Instance.Configuration.GetSection("RabbitMQ:UserName").Value;
            var password = ConfigurationReader.Instance.Configuration.GetSection("RabbitMQ:Password").Value;

            var factory = new ConnectionFactory
            {
                HostName = hostName,
                VirtualHost = virtualHost,
                Port = int.Parse(port),
                UserName = userName,
                Password = password
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange, exchangeType, true, false, null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: exchange,
                    routingKey: routingKey,
                    basicProperties: null,
                    body: body);

            Console.WriteLine($" [x] Sent {message} to exchange {exchange}, exchange type {exchangeType}");
        }
    }
}

