namespace OverlyComplicatedBowling.Shared.Dtos
{
	public class FrameDto
	{
		public SortedDictionary<int, RollDto> Rolls { get; set; }
		public int MaxRolls { get; set; }
		public int Score { get; set; }
		public int AccumulatedScore { get; set; }
		public bool Scored { get; set; }
		public bool Completed { get; set; }
		public int RemainingPins { get; set; }
		public int Index { get; set; }
	}
}
