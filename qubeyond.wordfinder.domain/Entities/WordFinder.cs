using qubeyond.wordfinder.shared.constant;

namespace qubeyond.wordfinder.domain.Entities
{
    public class WordFinder 
    {
        private const string MATRIX_PARAM_NAME = "matrix";
        private const int MAX_DIMENSION = 64;

        private readonly List<string> _rows;
        private readonly List<string> _columns;

        public List<string> Rows
        {
            get { return _rows; }
        }

        public List<string> Columns
        {
            get { return _columns; }
        }

        public WordFinder(IEnumerable<string> matrix)
        {
            Validation(matrix);

            _rows = matrix.ToList();

            int columnsToCreate = _rows[0].Length;
            _columns = [];
            for (int columnCont = 0; columnCont < columnsToCreate; columnCont++)
            {
                var column = new char[_rows.Count];
                for (int row = 0; row < _rows.Count; row++)
                {
                    column[row] = _rows[row][columnCont];
                }
                _columns.Add(new string(column));
            }
        }

        private static void Validation(IEnumerable<string> matrix)
        {
            if (matrix == null || !matrix.Any())
            {
                throw new ArgumentNullException(MATRIX_PARAM_NAME, ErrorMessages.MATRIX_NULL_OR_EMPTY);
            }

            int rowCount = matrix.Count();
            int columnCount = matrix.First().Length;

            // Verificamos si la cantidad de filas o columnas excede el límite de 64
            if (rowCount > MAX_DIMENSION || columnCount > MAX_DIMENSION)
            {
                throw new ArgumentException(ErrorMessages.MATRIX_EXCEEDS_MAX_DIMENSION);
            }

            // Validamos que todas las filas tengan la misma longitud
            bool sameLength = matrix.All(p => p.Length == columnCount);
            if (!sameLength)
            {
                throw new ArgumentException(ErrorMessages.MATRIX_INCONSISTENT_ROW_LENGTH);
            }
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

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var wordCounts = new Dictionary<string, int>();

            Parallel.ForEach(wordstream.Distinct(), word =>
            {
                int rowMatches = _rows.AsParallel().Sum(row => CountOccurrencesInString(row, word));
                int columnMatches = _columns.AsParallel().Sum(col => CountOccurrencesInString(col, word));

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
    }
}