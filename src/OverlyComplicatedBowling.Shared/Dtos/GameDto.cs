namespace OverlyComplicatedBowling.Shared.Dtos
{
	public class GameDto
	{
		public Guid Id { get; set; }
		public SortedDictionary<int, FrameDto> Frames { get; set; }
		public bool GameCompleted { get; set; }
		public int TotalScore { get; set; }
	}
}
