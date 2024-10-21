using Microsoft.AspNetCore.Mvc;
using qubeyond.wordfinder.api.ViewModels;
using qubeyond.wordfinder.domain.Contracts.Services;
using qubeyond.wordfinder.domain.Entities;

namespace qubeyond.wordfinder.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        /// This endpoint is in charge to retrieve the top 10 words found it.
        /// </summary>
        /// <param name="wordFinderViewModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet(Name = "word-finder")]
        public async Task<IActionResult> WordFinderAsync(WordFinderViewModel wordFinderViewModel)
        {
            WordFinder wordFinder = new(wordFinderViewModel.Matrix);
            IEnumerable<string> wordsFound = await _wordFinderService.FindAsync(wordFinder, wordFinderViewModel.WordStream);
            return Ok(wordsFound);
        }
    }
}
