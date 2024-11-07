using APIWolke.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace APIWolke.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<DeviceEntity> _devicesCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _devicesCollection = database.GetCollection<DeviceEntity>(mongoDBSettings.Value.CollectionName);
        }

        // Crear un nuevo dispositivo
        public async Task CreateAsync(DeviceEntity deviceEntity)
        {
            await _devicesCollection.InsertOneAsync(deviceEntity);
        }

        // Obtener todos los dispositivos
        public async Task<List<DeviceEntity>> GetDevicesAsync()
        {
            return await _devicesCollection.Find(new BsonDocument()).ToListAsync();
        }

        // Obtener un dispositivo por DeviceId
        public async Task<DeviceEntity?> GetDeviceAsync(string deviceId)
        {
            FilterDefinition<DeviceEntity> filter = Builders<DeviceEntity>.Filter.Eq(d => d.DeviceId, deviceId);
            return await _devicesCollection.Find(filter).FirstOrDefaultAsync();
        }

        // Actualizar un dispositivo existente
        public async Task UpdateDeviceAsync(string deviceId, DeviceEntity updatedDevice)
        {
            FilterDefinition<DeviceEntity> filter = Builders<DeviceEntity>.Filter.Eq(d => d.DeviceId, deviceId);
            await _devicesCollection.ReplaceOneAsync(filter, updatedDevice);
        }

        // Eliminar un dispositivo
        public async Task DeleteDeviceAsync(string deviceId)
        {
            FilterDefinition<DeviceEntity> filter = Builders<DeviceEntity>.Filter.Eq(d => d.DeviceId, deviceId);
            await _devicesCollection.DeleteOneAsync(filter);
        }
    }
}
