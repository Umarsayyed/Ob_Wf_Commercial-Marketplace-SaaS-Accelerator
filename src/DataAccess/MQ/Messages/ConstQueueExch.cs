namespace Marketplace.SaaS.Accelerator.DataAccess.MQ
{
    public static class QueueAndExchange
    {
        public static class Exchange
        {
            public static class PubSub
            {
                public static readonly string Exchange = "exn.pubsub.admin";                
                public static string Queue { get { return ConfigurationReader.Instance.Configuration.GetSection("RabbitMQ:PubSubQueue").Value; } }
            }
        }
    }
}