using qubeyond.wordfinder.domain.Contracts.Cache;
using qubeyond.wordfinder.domain.Contracts.Services;
using qubeyond.wordfinder.domain.Entities;

namespace qubeyond.wordfinder.domain.Services
{
    public class WordFinderService : IWordFinderService
    {
        private readonly ICacheProvider _cacheProvider;

        public WordFinderService(ICacheProvider cache) => _cacheProvider = cache;
        public async Task<IEnumerable<string>> FindAsync(WordFinder wordFinder, IEnumerable<string> wordstream)
        {
            string key = _cacheProvider.GetMatrixCacheKey(wordFinder.Rows, wordstream.Distinct().OrderDescending());
            IEnumerable<string>? listCached = await _cacheProvider.Get<IEnumerable<string>?>(key);
            if (listCached != null && listCached.Any())
            {
                return listCached;
            }
            listCached = wordFinder.Find(wordstream);
            await _cacheProvider.Add(key, listCached, null);
            return listCached;
        }
    }
}
