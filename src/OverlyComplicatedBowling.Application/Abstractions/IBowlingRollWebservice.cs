namespace OverlyComplicatedBowling.Application.Abstractions
{
    public interface IBowlingRollWebservice
    {
        Task<int> GetRollResultAsync(int remainingPins);
    }
}
