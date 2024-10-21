using qubeyond.wordfinder.domain.Entities;

namespace qubeyond.wordfinder.unitTest
{

    public class WordFinderTest
    {
        [Fact]
        public void Test_matrix_null()
        {
            IEnumerable<string>? matrix = null;
            var exception = Assert.Throws<ArgumentNullException>(() => new WordFinder(matrix));
            Assert.Equal("matrix", exception.ParamName);
        }

        [Fact]
        public void Test_matrix_string_more_than_64()
        {
            IEnumerable<string>? matrix = ["aaaaaaaaaabbbbbbbbbbbccccccccccdddddddddddeeeeeeeeeefffffffffffff"];
            Assert.Throws<ArgumentException>(() => new WordFinder(matrix));
        }


        [Fact]
        public void Test_matrix_works_limit_64()
        {
            string word = "test";
            List<string> matrix = [];
            for (int i = 3; i < 63; i++)
            {
                word += "a";
            }
            for (int i = 0; i < 64; i++)
            {
                matrix.Add(word);
            }

            WordFinder wordFinder = new(matrix);
            List<string> list = ["test", "told", "snow", "cold"];

            IEnumerable<string> words = wordFinder.Find(list);
            Assert.Single(words);
            Assert.Equal("test", words.Single());
        }

        [Fact]
        public void Test_matrix_limit_65_failed()
        {
            string word = "test";
            List<string> matrix = [];
            for (int i = 3; i < 63; i++)
            {
                word += "a";
            }
            for (int i = 0; i < 65; i++)
            {
                matrix.Add(word);
            }

            Assert.Throws<ArgumentException>(() => new WordFinder(matrix));
        }

        [Fact]
        public void TestFinderNxNOk()
        {
            IEnumerable<string> matrix = ["snowc", "coldo", "coldl", "s1owd", "snowt"];

            List<string> list = ["cold", "told", "snow", "cold"];
            WordFinder wf = new(matrix);
            IEnumerable<string> resultado = wf.Find(list);
            Assert.Equal(2, resultado.Count());
            Assert.Equal("cold", resultado.First());
            Assert.Equal("snow", resultado.Single(x => "snow".Equals(x)));
        }
        [Fact]
        public void TestFinderNxMOk()
        {
            IEnumerable<string> matrix = ["snow", "cold", "cold", "s1ow", "snow"];

            List<string> list = ["cold", "told", "snow", "cold"];
            WordFinder wf = new(matrix);
            IEnumerable<string> resultado = wf.Find(list);
            Assert.Equal(2, resultado.Count());
            Assert.Equal("cold", resultado.First());
            Assert.Equal("snow", resultado.Single(x => "snow".Equals(x)));
        }
    }
}
