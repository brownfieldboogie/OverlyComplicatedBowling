using Microsoft.AspNetCore.WebUtilities;
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

		public async Task<MatchDto> StartMatchAsync(int numberOfPlayers)
		{
			var url = QueryHelpers.AddQueryString("StartMatch", new Dictionary<string, string?>
			{
				{ "numberOfPlayers", numberOfPlayers.ToString() }
			});


			var result = await _httpClient.GetAsync(url);

			result.EnsureSuccessStatusCode();

			var resultContent = await result.Content.ReadAsStringAsync();
			var resultContentConverted = JsonConvert.DeserializeObject<MatchDto>(resultContent) ?? throw new InvalidOperationException();

			return resultContentConverted;
		}

		public async Task<MatchDto> AddRollAsync(Guid matchId, Guid gameId)
		{
			var result = await _httpClient.PostAsync($"/AddRoll/{matchId}/{gameId}", null);

			result.EnsureSuccessStatusCode();

			var resultContent = await result.Content.ReadAsStringAsync();
			var resultContentConverted = JsonConvert.DeserializeObject<MatchDto>(resultContent) ?? throw new InvalidOperationException();

			return resultContentConverted;
		}
	}
}
