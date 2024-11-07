using System.Text.Json.Nodes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using APIWolke.Models;
using APIWolke.Dtos;
using System.Collections.Generic;

namespace APIWolke.Mapping
{
    public class DeviceMapping
    {
        public static DeviceEntity MapToEntity(DeviceDto dto)
        {
            return new DeviceEntity
            {
                Id = dto.Id,
                DeviceId = dto.DeviceId,
                Name = dto.Name,
                MinFwVersion = dto.MinFwVersion,
                MinHwVersion = dto.MinHwVersion,
                NumElements = dto.NumElements,
                Tabs = dto.Tabs.ConvertAll(MapTabToEntity)
            };
        }

        public static DeviceDto MapToDto(DeviceEntity entity)
        {
            return new DeviceDto
            {
                Id = entity.Id,
                DeviceId = entity.DeviceId,
                Name = entity.Name,
                MinFwVersion = entity.MinFwVersion,
                MinHwVersion = entity.MinHwVersion,
                NumElements = entity.NumElements,
                Tabs = entity.Tabs.ConvertAll(MapTabToDto)
            };
        }

        private static TabEntity MapTabToEntity(TabDto dto)
        {
            return new TabEntity
            {
                Id = dto.Id,
                Label = dto.Label,
                Enabled = dto.Enabled,
                Visible = dto.Visible,
                Elements = dto.Elements.ConvertAll(MapElementToEntity)
            };
        }

        private static TabDto MapTabToDto(TabEntity entity)
        {
            return new TabDto
            {
                Id = entity.Id,
                Label = entity.Label,
                Enabled = entity.Enabled,
                Visible = entity.Visible,
                Elements = entity.Elements.ConvertAll(MapElementToDto)
            };
        }

        private static ElementEntity MapElementToEntity(ElementDto dto)
        {
            return new ElementEntity
            {
                Id = dto.Id,
                Label = dto.Label,
                Enabled = dto.Enabled,
                Visible = dto.Visible,
                ReadOnly = dto.ReadOnly,
                Type = dto.Type,
                Length = dto.Length,
                Offset = dto.Offset,
                Options = dto.Options != null ? BsonSerializer.Deserialize<BsonDocument>(dto.Options.ToString()!) : new BsonDocument()
            };
        }

        private static ElementDto MapElementToDto(ElementEntity entity)
        {
            return new ElementDto
            {
                Id = entity.Id,
                Label = entity.Label,
                Enabled = entity.Enabled,
                Visible = entity.Visible,
                ReadOnly = entity.ReadOnly,
                Type = entity.Type,
                Length = entity.Length,
                Offset = entity.Offset,
                Options = entity.Options != null ? JsonNode.Parse(entity.Options.ToString()!) : null
            };
        }
    }
}
