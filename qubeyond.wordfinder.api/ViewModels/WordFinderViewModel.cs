namespace qubeyond.wordfinder.api.ViewModels
{
    public class WordFinderViewModel
    {
        public IEnumerable<string> Matrix { get; set; } = [];
        public IEnumerable<string> WordStream { get; set; } = [];
    }
}
