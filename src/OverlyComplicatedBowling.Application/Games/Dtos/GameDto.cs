namespace OverlyComplicatedBowling.Application.Games.Dtos
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public Dictionary<int, FrameDto> Frames { get; set; }
        public bool GameCompleted { get; set; }
    }
}
