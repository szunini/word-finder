﻿using Moq;
using qubeyond.wordfinder.domain.Contracts.Cache;
using qubeyond.wordfinder.domain.Entities;
using qubeyond.wordfinder.domain.Services;
using quebeyond.wordfinder.infraestructure.Validation;



namespace qubeyond.wordfinder.unitTest
{
    public class WordFinderServiceTest
    {
        private readonly Mock<ICacheProvider> _cacheMock = new();

        [Fact]
        public async Task TestIsCachedOk()
        {
            List<string> matrix = ["snowc", "coldo", "coldl", "s1owd", "snowt"];

            List<string> list = ["cold", "told", "snow", "cold"];
            WordFinder wf = new(matrix);
            IEnumerable<string> returnCached = ["snowc"];

            _cacheMock
                .Setup(c => c.GetMatrixCacheKey(It.IsAny<IEnumerable<string>>(), It.IsAny<IEnumerable<string>>()))
                .Returns("key");

            _cacheMock
                .Setup(c => c.Get<IEnumerable<string>?>("key"))
                .ReturnsAsync(returnCached);

            _cacheMock
                .Setup(c => c.Add("key", wf.Find(list), null))
                .ReturnsAsync(true);

            WordFinderService wordFinderService = new(_cacheMock.Object);
            IEnumerable<string> test = await wordFinderService.FindAsync(wf, list);
            Assert.Equal(returnCached, test);
        }

        [Fact]
        public async Task TestIsNotCachedOk()
        {
            List<string> matrix = ["snowc", "coldo", "coldl", "s1owd", "snowt"];

            List<string> list = ["cold", "told", "snow", "cold"];
            WordFinder wf = new(matrix);
            IEnumerable<string>? returnCached = null;
            _cacheMock
                .Setup(c => c.GetMatrixCacheKey(It.IsAny<IEnumerable<string>>(), It.IsAny<IEnumerable<string>>()))
                .Returns("key");

            _cacheMock
                .Setup(c => c.Get<IEnumerable<string>?>("key"))

                .ReturnsAsync(returnCached);

            _cacheMock
                .Setup(c => c.Add("key", wf.Find(list), null))
                .ReturnsAsync(true);

            WordFinderService wordFinderService = new(_cacheMock.Object);
            IEnumerable<string> test = await wordFinderService.FindAsync(wf, list);
            Assert.Equal(wf.Find(list), test);
        }

        [Fact]
        public async Task TestIsNotCachedValidatorOk()
        {
            List<string> matrix = ["snowc", "coldo", "coldl", "s1owd", "snowt"];

            List<string> list = ["cold", "told", "snow", "cold"];
            WordFinder wf = new(matrix);
            IEnumerable<string>? returnCached = null;
            _cacheMock
                .Setup(c => c.GetMatrixCacheKey(It.IsAny<IEnumerable<string>>(), It.IsAny<IEnumerable<string>>()))
                .Returns("key");

            _cacheMock
                .Setup(c => c.Get<IEnumerable<string>?>("key"))

                .ReturnsAsync(returnCached);

            _cacheMock
                .Setup(c => c.Add("key", wf.Find(list), null))
                .ReturnsAsync(true);

            WordFinderServiceWithValidator wordFinderService = new(_cacheMock.Object, new WordFinderValidator());
            IEnumerable<string> test = await wordFinderService.FindAsync(wf, list);
            Assert.Equal(wf.Find(list), test);
        }
    }
}
