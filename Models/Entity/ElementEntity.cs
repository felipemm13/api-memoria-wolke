using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace APIWolke.Models
{
    public class ElementEntity
    {
        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("label")]
        public string Label { get; set; } = string.Empty;

        [BsonElement("enabled")]
        public bool Enabled { get; set; }

        [BsonElement("visible")]
        public bool Visible { get; set; }

        [BsonElement("readOnly")]
        public bool ReadOnly { get; set; }

        [BsonElement("type")]
        public string Type { get; set; } = string.Empty;

        [BsonElement("length")]
        public int Length { get; set; }

        [BsonElement("offset")]
        public int Offset { get; set; }

        [BsonElement("options")]
        public BsonDocument Options { get; set; } = new BsonDocument();
    }
}
