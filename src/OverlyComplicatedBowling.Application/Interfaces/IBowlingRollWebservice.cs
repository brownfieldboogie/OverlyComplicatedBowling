namespace OverlyComplicatedBowling.Application.Interfaces
{
    public interface IBowlingRollWebservice
    {
        Task<int> GetRollResultAsync(int remainingPins);
    }
}
