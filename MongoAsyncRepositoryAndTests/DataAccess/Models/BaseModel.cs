using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DataAccess.Models
{
    public class BaseModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
