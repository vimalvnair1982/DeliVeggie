﻿using RabbitMQ.Client;

namespace DeliVeggieProducer.Model
{
    public class MongoDbSettings
    {
        public string ConnectionURI { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

    }
}
