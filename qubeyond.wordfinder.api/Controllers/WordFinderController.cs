using Microsoft.AspNetCore.Mvc;
using qubeyond.wordfinder.api.ViewModels;
using qubeyond.wordfinder.domain.Contracts.Services;
using qubeyond.wordfinder.domain.Entities;

namespace qubeyond.wordfinder.api.Controllers
{
    [ApiController]
    [Route("api/word-finder")]
    public class WordFinderController : ControllerBase
    {
        private readonly ILogger<WordFinderController> _logger;
        private readonly IWordFinderService _wordFinderService;

        public WordFinderController(ILogger<WordFinderController> logger, IWordFinderService wordFinderService)
        {
            _logger = logger;
            _wordFinderService = wordFinderService;
        }

        /// <summary>
        /// Retrieves the top 10 most frequent words found in the provided matrix and word stream.
        /// </summary>
        /// <param name="wordFinderViewModel">The input data containing the character matrix and word stream.</param>        
        /// <remarks>
        ///  Sample Request:
        /// 
        ///   
        ///        POST api/word-finder
        ///        
        ///        
        ///        {
        ///        
        ///        "matrix":["snow", "cold", "cold", "s1ow", "snow"],
        ///        
        ///        "wordStream": ["cold", "told", "snow", "cold"]
        ///        
        ///        }
        /// </remarks>
        /// <returns>A list of the top 10 words found in the matrix as max.</returns>
        /// <response code ="200">Returns the list of top 10 words.</response>
        /// <response code ="400">The matrix or word stream does not meet the required specification </response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpPost]
        public async Task<IActionResult> WordFinderAsync( WordFinderViewModel wordFinderViewModel)
        {
            WordFinder wordFinder = new(wordFinderViewModel.Matrix);
            IEnumerable<string> wordsFound = await _wordFinderService.FindAsync(wordFinder, wordFinderViewModel.WordStream);
            return Ok(wordsFound);
        }
    }
}
