using Newtonsoft.Json;
using qubeyond.wordfinder.domain.Contracts.Cache;
using StackExchange.Redis;

namespace quebeyond.wordfinder.infraestructure.Cache
{

    public class RedisCacheProvider : ICacheProvider
    {
        private readonly IDatabase _database;
        private const string JoinMatrixDelimiter = "_"; 
        public RedisCacheProvider(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<T?> Get<T>(string key)
        {
            RedisValue value = await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? default(T?) : JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<bool> Add<T>(string key, T value, TimeSpan? expiry)
        {
             return await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), expiry);
        }

        public async Task<bool> Delete(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public string GetMatrixCacheKey(IEnumerable<string> matrix, IEnumerable<string> wordStream)
        {
            string matrixKey = string.Join(JoinMatrixDelimiter, matrix);
            string wordStreamKey = string.Join(JoinMatrixDelimiter, wordStream.Distinct().OrderDescending());
            
            return $"{matrixKey}_{wordStreamKey}";
        }


    }
}
