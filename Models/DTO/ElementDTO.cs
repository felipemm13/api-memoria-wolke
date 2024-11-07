using System.Text.Json.Nodes;

namespace APIWolke.Dtos
{
    public class ElementDto
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public bool ReadOnly { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Length { get; set; }
        public int Offset { get; set; }
        public JsonNode? Options { get; set; }
    }
}
