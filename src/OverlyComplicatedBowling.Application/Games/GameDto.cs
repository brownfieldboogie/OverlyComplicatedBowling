namespace OverlyComplicatedBowling.Application.Games
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public Dictionary<int, FrameDto> Frames { get; set; }
    }
}
