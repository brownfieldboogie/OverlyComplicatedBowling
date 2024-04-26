namespace OverlyComplicatedBowling.Application.Games
{
    public class FrameDto
    {
        public Dictionary<int, RollDto> Rolls { get; set; }
        public int MaxRolls { get; set; }
        public int Score { get; set; }
        public bool Scored { get; set; }
        public bool Completed { get; set; }
        public int RemainingPins { get; set; }
    }
}
