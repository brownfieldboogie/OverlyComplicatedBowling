using Newtonsoft.Json;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Presentation.Web.Services
{
	public class OverlyComplicatedBowlingService : IOverlyComplicatedBowlingService
	{
		private readonly HttpClient _httpClient;

		public OverlyComplicatedBowlingService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<GameDto> StartGameAsync()
		{
			var result = await _httpClient.GetAsync("/StartGame");
			var resultContent = await result.Content.ReadAsStringAsync();
			var resultContentConverted = JsonConvert.DeserializeObject<GameDto>(resultContent);

			return resultContentConverted;
		}

		public async Task<GameDto> AddRollAsync(Guid gameId)
		{
			var result = await _httpClient.PostAsync($"/AddRoll/{gameId}", null);
			var resultContent = await result.Content.ReadAsStringAsync();
			var resultContentConverted = JsonConvert.DeserializeObject<GameDto>(resultContent);

			return resultContentConverted;
		}
	}
}
