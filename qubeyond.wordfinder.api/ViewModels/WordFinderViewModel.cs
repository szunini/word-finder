using System.ComponentModel.DataAnnotations;

namespace qubeyond.wordfinder.api.ViewModels
{
    public class WordFinderViewModel
    {
        [Required]
        public IEnumerable<string> Matrix { get; set; } = [];

        [Required]
        public IEnumerable<string> WordStream { get; set; } = [];
    }
}
