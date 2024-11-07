using Microsoft.AspNetCore.Mvc;
using APIWolke.Models;
using APIWolke.Services;
using APIWolke.Dtos; // Importar DeviceDto
using APIWolke.Mapping; // Importar DeviceMapping
using System.Threading.Tasks;
using System.Collections.Generic;

namespace APIWolke.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;

        public DevicesController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        // GET: api/devices
        [HttpGet]
        public async Task<ActionResult<List<DeviceDto>>> GetDevices()
        {
            var deviceEntities = await _mongoDBService.GetDevicesAsync();
            var deviceDtos = deviceEntities.ConvertAll(DeviceMapping.MapToDto);
            return Ok(deviceDtos);
        }

        // GET: api/devices/{deviceId}
        [HttpGet("{deviceId}")]
        public async Task<ActionResult<DeviceDto>> GetDevice(string deviceId)
        {
            var deviceEntity = await _mongoDBService.GetDeviceAsync(deviceId);

            if (deviceEntity is null)
            {
                return NotFound(new { Message = $"Device with DeviceId '{deviceId}' not found." });
            }

            var deviceDto = DeviceMapping.MapToDto(deviceEntity);
            return Ok(deviceDto);
        }

        // POST: api/devices
        [HttpPost]
        public async Task<ActionResult<DeviceDto>> AddDevice([FromBody] DeviceDto deviceDto)
        {
            if (deviceDto == null || string.IsNullOrEmpty(deviceDto.DeviceId))
            {
                return BadRequest(new { Message = "Device data is invalid." });
            }

            var existingDevice = await _mongoDBService.GetDeviceAsync(deviceDto.DeviceId);
            if (existingDevice is not null)
            {
                return Conflict(new { Message = $"Device with DeviceId '{deviceDto.DeviceId}' already exists." });
            }

            var deviceEntity = DeviceMapping.MapToEntity(deviceDto);
            await _mongoDBService.CreateAsync(deviceEntity);
            var createdDeviceDto = DeviceMapping.MapToDto(deviceEntity);
            return CreatedAtAction(nameof(GetDevice), new { deviceId = createdDeviceDto.DeviceId }, createdDeviceDto);
        }

        // PUT: api/devices/{deviceId}
        [HttpPut("{deviceId}")]
        public async Task<ActionResult> UpdateDevice(string deviceId, [FromBody] DeviceDto updatedDeviceDto)
        {
            if (updatedDeviceDto == null)
            {
                return BadRequest(new { Message = "Updated device data is invalid." });
            }

            var existingDevice = await _mongoDBService.GetDeviceAsync(deviceId);
            if (existingDevice is null)
            {
                return NotFound(new { Message = $"Device with DeviceId '{deviceId}' not found." });
            }

            var updatedDeviceEntity = DeviceMapping.MapToEntity(updatedDeviceDto);
            updatedDeviceEntity.Id = existingDevice.Id; // Ensure the Id remains the same
            updatedDeviceEntity.DeviceId = deviceId; // Ensure the DeviceId remains consistent

            await _mongoDBService.UpdateDeviceAsync(deviceId, updatedDeviceEntity);
            return NoContent();
        }

        // DELETE: api/devices/{deviceId}
        [HttpDelete("{deviceId}")]
        public async Task<ActionResult> DeleteDevice(string deviceId)
        {
            var existingDevice = await _mongoDBService.GetDeviceAsync(deviceId);
            if (existingDevice is null)
            {
                return NotFound(new { Message = $"Device with DeviceId '{deviceId}' not found." });
            }

            await _mongoDBService.DeleteDeviceAsync(deviceId);
            return NoContent();
        }
    }
}
