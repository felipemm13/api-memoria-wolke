using System.Collections.Generic;

namespace APIWolke.Dtos
{
    public class DeviceDto
    {
        public string? Id { get; set; }
        public string DeviceId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string MinFwVersion { get; set; } = string.Empty;
        public string MinHwVersion { get; set; } = string.Empty;
        public int NumElements { get; set; }
        public List<TabDto> Tabs { get; set; } = new List<TabDto>();
    }
}
