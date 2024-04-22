using OverlyComplicatedBowling.Application.Abstractions;

namespace OverlyComplicatedBowling.Infrastructure.Scraping
{
    public class BowlingRollWebservice : IBowlingRollWebservice
    {
        private HttpClient _httpClient { get; set; }

        public BowlingRollWebservice(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("http://www.randomnumberapi.com/api/v1.0/"); //move to appsettings?
        }

        public async Task<int> GetRollResultAsync(int remainingPins)
        {
            var requestUri = CreateParameterString(remainingPins);
            var result = await _httpClient.GetAsync(requestUri);
            var roll = ConvertResult(await result.Content.ReadAsStringAsync());

            return roll;
        }

        private string CreateParameterString(int remainingPins)
        {
            var queryParameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("min", "0"),
                new KeyValuePair<string, string>("max", remainingPins.ToString()),
                new KeyValuePair<string, string>("count", "1")
            };

            return "random?" + string.Join("&", queryParameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }

        private int ConvertResult(string result)
        {
            string resultTrimmed = result.Trim(new char[] { '[', ']', '\n' });

            if (int.TryParse(resultTrimmed, out int resultInt))
            {
                return resultInt;
            }
            else
            {
                throw new FormatException("The string could not be converted to an integer.");
            }
        }
    }
}
