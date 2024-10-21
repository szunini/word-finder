namespace qubeyond.wordfinder.domain.Contracts.Validators
{
    public interface IWordFinderValidator
    {
        /// <summary>
        /// This is a fluent validator but it is not used, it is just in case that 
        /// the implementation of the WordFinder class could be changed.
        /// </summary>
        /// <param name="matrix"></param>
        void Validate(IEnumerable<string> matrix);
    }
}
