using FluentAssertions;
using OverlyComplicatedBowling.Infrastructure.Scraping;

namespace OverlyComplicatedBowling.Infrastructure.Tests.ExternalServices
{
    [TestClass]
    public class BowlingRollGeneratorTests
    {
        private BowlingRollWebservice _rollGenerator;

        [TestInitialize]
        public void Initialize()
        {
            var httpClient = new HttpClient();

            _rollGenerator = new BowlingRollWebservice(httpClient);
        }

        [TestMethod]
        public async Task GetRollResultAsync_ReturnsResultWithinBounds()
        {
            //Arrange
            var max = 10;
            var numberOfResults = 10;
            var results = new int[numberOfResults];

            //Act
            for (int i = 0; i < numberOfResults; i++)
            {
                results[i] = await _rollGenerator.GetRollResultAsync(max);
            }

            //Assert
            results.Should().HaveCount(numberOfResults);
            results.Should().AllSatisfy(r => r.Should().BeLessThanOrEqualTo(10));
        }
    }
}