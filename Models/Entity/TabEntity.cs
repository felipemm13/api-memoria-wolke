using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace APIWolke.Models
{
    public class TabEntity
    {
        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("label")]
        public string Label { get; set; } = string.Empty;

        [BsonElement("enabled")]
        public bool Enabled { get; set; }

        [BsonElement("visible")]
        public bool Visible { get; set; }

        [BsonElement("elements")]
        public List<ElementEntity> Elements { get; set; } = new List<ElementEntity>();
    }
}
