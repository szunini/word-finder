using qubeyond.wordfinder.domain.Entities;

namespace qubeyond.wordfinder.domain.Contracts.Services
{
    public interface IWordFinderService
    {
        /// <summary>
        /// Returns the top 10 words of the wordstreams that exist in the wordfinder matrix.
        /// </summary>
        /// <param name="wordFinder"></param>
        /// <param name="wordstream"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> FindAsync(WordFinder wordFinder, IEnumerable<string> wordstream);
    }
}
