using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace APIWolke.Models
{
    public class DeviceEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string? Id { get; set; }

        [BsonElement("deviceId")]
        public string DeviceId { get; set; } = string.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("minFwVersion")]
        public string MinFwVersion { get; set; } = string.Empty;

        [BsonElement("minHwVersion")]
        public string MinHwVersion { get; set; } = string.Empty;

        [BsonElement("numElements")]
        public int NumElements { get; set; }

        [BsonElement("tabs")]
        public List<TabEntity> Tabs { get; set; } = new List<TabEntity>();
    }
}
