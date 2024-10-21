namespace qubeyond.wordfinder.domain.Contracts.Cache
{
    /// <summary>
    /// Interface to use a service cache
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// Given a key retrieves the object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T?> Get<T>( string key);

        /// <summary>
        /// Given a key and object you add it in the cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> Add<T>(string key, T value, TimeSpan? expiry);

        Task<bool> Delete(string key);

        /// <summary>
        /// Creates the cache key for the matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="wordStream"></param>
        /// <returns></returns>
        string GetMatrixCacheKey(IEnumerable<string> matrix, IEnumerable<string> wordStream);
    }
}
