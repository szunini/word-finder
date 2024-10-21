using qubeyond.wordfinder.domain.Contracts.Cache;
using qubeyond.wordfinder.domain.Contracts.Services;
using qubeyond.wordfinder.domain.Contracts.Validators;
using qubeyond.wordfinder.domain.Entities;

namespace qubeyond.wordfinder.domain.Services
{
    //This class is a second implementation, in this moment it doesn't have use
    // but as  don't have time to 

    public class WordFinderServiceWithValidator : IWordFinderService
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IWordFinderValidator _wordFinderValidator;
        public WordFinderServiceWithValidator(ICacheProvider cache, IWordFinderValidator validator)
        {
            _cacheProvider = cache;
            _wordFinderValidator = validator;
        }

        public async Task<IEnumerable<string>> FindAsync(WordFinder wordFinder, IEnumerable<string> wordstream)
        {
            _wordFinderValidator.Validate(wordFinder.Rows);
            string key = _cacheProvider.GetMatrixCacheKey(wordFinder.Rows, wordstream.Distinct().OrderDescending());
            IEnumerable<string>? listCached = await _cacheProvider.Get<IEnumerable<string>?>(key);
            if (listCached != null && listCached.Any())
            {
                return listCached;
            }
            listCached = Find(wordFinder, wordstream);
            await _cacheProvider.Add(key, listCached, null);
            return listCached;
        }

        private IEnumerable<string> Find(WordFinder wordFinder, IEnumerable<string> wordstream)
        {
            var wordCounts = new Dictionary<string, int>();

            Parallel.ForEach(wordstream.Distinct(), word =>
            {
                int rowMatches = wordFinder.Rows.AsParallel().Sum(row => CountOccurrencesInString(row, word));
                int columnMatches = wordFinder.Columns.AsParallel().Sum(col => CountOccurrencesInString(col, word));

                int totalMatches = rowMatches + columnMatches;
                if (totalMatches > 0)
                {
                    lock (wordCounts)
                    {
                        wordCounts[word] = totalMatches;
                    }
                }
            });

            var result = wordCounts
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key)
                .Take(10)
                .Select(kvp => kvp.Key);

            return result;
        }

        private static int CountOccurrencesInString(string str, string word)
        {
            int count = 0;
            int index = 0;


            while ((index = str.IndexOf(word, index, StringComparison.Ordinal)) != -1)
            {
                count++;
                index++;
            }

            return count;
        }

    }
}
