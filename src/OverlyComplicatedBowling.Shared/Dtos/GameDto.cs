namespace OverlyComplicatedBowling.Shared.Dtos
{
	public class GameDto
	{
		public Guid Id { get; set; }
		public List<FrameDto> Frames { get; set; }
		public bool GameCompleted { get; set; }
		public int TotalScore { get; set; }
		public int Index { get; set; }
	}
}
