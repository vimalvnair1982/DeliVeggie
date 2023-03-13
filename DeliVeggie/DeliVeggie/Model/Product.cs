using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace DeliVeggie.Model
{
    public class Product
    {
        [BsonId]
       // [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? Price { get; set; }


    }
}
