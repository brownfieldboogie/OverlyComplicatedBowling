namespace OverlyComplicatedBowling.Shared.Dtos
{
	public class MatchDto
	{
		public Guid Id { get; set; }
		public SortedDictionary<int, GameDto> Games { get; set; }
		public Guid IdOfActiveGame { get; set; }
	}
}
