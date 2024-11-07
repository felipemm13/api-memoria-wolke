using System.Collections.Generic;

namespace APIWolke.Dtos
{
    public class TabDto
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public List<ElementDto> Elements { get; set; } = new List<ElementDto>();
    }
}
